using EmguTest.Service;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.ListView;

namespace EmguTest
{
    public partial class MachineTrainingFrom : Form
    {
        public InitialData InitialData { get; set; } = new InitialData();
        public static string initialDataPath = "InitialData.json";
        public MachineTrainingFrom()
        {
            InitializeComponent();
        }

        private void MachineTrainingFrom_Load(object sender, EventArgs e)
        {
            if (File.Exists(initialDataPath))
            {
                this.InitialData = JsonConvert.DeserializeObject<InitialData>(File.ReadAllText(initialDataPath));

                this.tb_trainLibName.Text = this.InitialData.TrainingLibName;
                this.tb_samplePath.Text = this.InitialData.SamplePath;
                this.tb_tessPath.Text = this.InitialData.TesseractPath;
            }

            FillLangsData();
        }

        private void MachineTrainingFrom_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.InitialData.SamplePath = this.tb_samplePath.Text.Trim();
            this.InitialData.TesseractPath = this.tb_tessPath.Text.Trim();
            this.InitialData.TrainingLibName = this.tb_trainLibName.Text.Trim();

            var jsonData = Newtonsoft.Json.JsonConvert.SerializeObject(this.InitialData);
            File.WriteAllText(initialDataPath, jsonData);
                
        }

        private void Bt_samplePathChose_Click(object sender, EventArgs e)
        {
        }

        private void Bt_refreshSamples_Click(object sender, EventArgs e)
        {

            this.lv_sapmoles.Items.Clear();

            DirectoryInfo sampleDir = new DirectoryInfo(this.tb_samplePath.Text);
            var tifFiles = sampleDir.GetFiles().Where(f => f.Extension == ".tif");
            foreach (var item in tifFiles)
            {
                this.lv_sapmoles.Items.Add(item.Name, item.Name, item.FullName);
                //this.tr_sample.Nodes.Add(item.FullName, item.Name);
            }
        }

        private void Btn_productBox_Click(object sender, EventArgs e)
        {
            var nodes = this.lv_sapmoles.SelectedItems;
            if (nodes == null || nodes.Count == 0)
            {
                MessageBox.Show("未选择图片文件");
                return;
            }
            StringBuilder operateStr = new StringBuilder();
            foreach (ListViewItem item in nodes)
            {
                string lang = this.cb_langs.SelectedItem.ToString();
                var command = $"tesseract {item.Text} {Path.GetFileNameWithoutExtension(item.Text)} -l {lang} batch.nochop makebox";
                operateStr.AppendLine(command);

                //var result=Utility.ExecCMD(command);
                //Console.WriteLine(result);
            }
            var filePath = $"{this.tb_samplePath.Text}\\makebox.bat";
            ExeBat(operateStr, filePath);

            //刷新Box 的listview
            Bt_refreshBox_Click(null, null);

            var resultDialog = MessageBox.Show("Box文件生成成功;\r\n 可以人工效验了,是否打开jTessBoxEditor", "Box生成", MessageBoxButtons.YesNo);

            if (resultDialog == DialogResult.Yes)
            {
                string startJtPath = $"{this.tb_tessPath.Text}\\jTessBoxEditor\\train.bat";
                ProcessStartInfo processStartInfo = new ProcessStartInfo(startJtPath) { WorkingDirectory = $"{this.tb_tessPath.Text}\\jTessBoxEditor\\" };
                var pro = Process.Start(processStartInfo);
                //Process.Start(processStartInfo);
            }

        }

        private void ExeBat(StringBuilder operateStr, string filePath)
        {
            ExeBat(operateStr.ToString(), filePath);
            
        }
        private void ExeBat(string operateStr, string filePath)
        {
            if (filePath.IndexOf(":") == -1)
            {
                filePath = $"{this.tb_samplePath.Text}\\{filePath}";
            }
            File.WriteAllText(filePath, operateStr);
            ProcessStartInfo processStartInfo = new ProcessStartInfo(filePath) { WorkingDirectory = this.tb_samplePath.Text };
            var pro = Process.Start(processStartInfo);
            pro.WaitForExit();
        }

        public void FillLangsData()
        {
            this.cb_langs.Items.Clear();

            var getLangCommand = "tesseract --list-langs";
            var langs = Utility.ExecCMD(getLangCommand);
            var temIndx = langs.IndexOf("tesseract --list-langs");
            langs = langs.Substring(temIndx);
            var startIndex = langs.IndexOf(':');
            var endIndex = langs.LastIndexOf(':');
            langs = langs.Remove(endIndex - 1);
            langs=langs.Substring(startIndex + 1);
            var langArray = langs.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var item in langArray)
            {
                this.cb_langs.Items.Add(item);
            }
            var trainLibName= this.InitialData.TrainingLibName;
            this.cb_langs.SelectedIndex = Array.IndexOf(langArray, trainLibName);
            
        }

        private void Bt_refreshTr_Click(object sender, EventArgs e)
        {

            var nodes = this.lv_box.SelectedItems;
            if (nodes == null || nodes.Count == 0)
            {
                return;
            }

            this.lv_tr.Items.Clear();

            foreach (ListViewItem item in nodes)
            {
                string trName = Path.GetFileNameWithoutExtension(item.Text) + ".tr";
                if (File.Exists($"{this.tb_samplePath.Text}\\{trName}"))
                {
                    this.lv_tr.Items.Add(trName);
                }
            }

        }

        private void Bt_refreshBox_Click(object sender, EventArgs e)
        {
            var nodes = this.lv_sapmoles.SelectedItems;
            if (nodes == null || nodes.Count == 0)
            {
                return;
            }

            this.lv_box.Items.Clear();

            foreach (ListViewItem item in nodes)
            {
                string boxName = Path.GetFileNameWithoutExtension(item.Text) + ".box";
                if (File.Exists($"{this.tb_samplePath.Text}\\{boxName}"))
                {
                    this.lv_box.Items.Add(boxName);
                }
            }
            
           
        }

        private void Bt_proTr_Click(object sender, EventArgs e)
        {
            var boxList = this.lv_box.Items;

            StringBuilder operateStr = new StringBuilder();
            foreach (ListViewItem item in boxList)
            {
                var tmpName = Path.GetFileNameWithoutExtension(item.Text);
                string command = $"tesseract  {tmpName}.tif {tmpName}  nobatch box.train";
                operateStr.AppendLine(command);
                //var cmdResult = Utility.ExecCMD(command);
                //Console.WriteLine(cmdResult);
            }
            var filePath = $"{this.tb_samplePath.Text}\\proTr_boxtrain.bat";
            ExeBat(operateStr, filePath);
            //File.WriteAllText( filePath, operateStr.ToString());
            //var pro = Process.Start(filePath);
            //pro.WaitForExit();

            Bt_refreshTr_Click(null, null);

            MessageBox.Show("TR生成完毕");
        }

        private void Bt_getChar_Click(object sender, EventArgs e)
        {
            var nodes = this.lv_box.SelectedItems;
            if (nodes == null || nodes.Count == 0)
            {
                MessageBox.Show("没有发现有选中的Box文件");
                return;
            }
            this.lv_trainResult.Items.Clear();
            //1、生成tr
            Bt_proTr_Click(null, null);
            //2、抽取字符
            ProductUniCharset(nodes);
            //3、生成Shape文件
            Shapeclustering(nodes);
            //4、生成聚集字符特征文件，mftraining
            Mftraining(nodes);
            //5、合并所有tr文件，cntraining
            Cntraining(nodes);

            var resultDialog = MessageBox.Show("所有工作准备完毕,是否生成训练库", "准备完成", MessageBoxButtons.YesNo);
            if(resultDialog== DialogResult.No)
            {
                return;
            }

            //6、修改名称
            ReName();
            //7、生成训练库
            CombineTessdata();
            //8、把训练好的字体库，复制到tesseract字库目录
            //var traineddataName= $"{this.tb_trainLibName}.traineddata";
            //File.Copy(traineddataName, $"{this.tb_tessPath.Text}\\tessdata\\{traineddataName}");
            //刷新字库
            FillLangsData();



        }

        private void ProductUniCharset(SelectedListViewItemCollection nodes)
        {
            
            
            string boxArrayStr = "";
            foreach (ListViewItem item in nodes)
            {
                if (File.Exists($"{this.tb_samplePath.Text}\\{item.Text}"))
                {
                    boxArrayStr += $"{item.Text} ";

                }

            }
            if (string.IsNullOrEmpty(boxArrayStr))
            {
                MessageBox.Show("没有检查到有任何box文件", "抽取字符");
                return;
            }
            CheckFont_properties();

            string command = $"unicharset_extractor {boxArrayStr.TrimEnd()}";

            var filePath = $"proChart.bat";
            ExeBat(command, filePath);
            //File.WriteAllText(filePath, command);
            //var pro = Process.Start(filePath);
            //pro.WaitForExit();
            //var cmdResult = Utility.ExecCMD(command);
            //Console.WriteLine(cmdResult);

            this.lv_trainResult.Items.Add("unicharset");
            MessageBox.Show("从Box中抽取字符完成");
        }

        private void CheckFont_properties()
        {
            var fileName = $"{this.tb_samplePath.Text}\\font_properties.txt";
            File.WriteAllText(fileName, "font 0 0 0 0 0");
            
        }

        private void Shapeclustering(SelectedListViewItemCollection nodes)
        {
            string trArrayStr = "";
            foreach (ListViewItem item in nodes)
            {
                string trName = $"{ Path.GetFileNameWithoutExtension(item.Text)}.tr";
                if (File.Exists($"{this.tb_samplePath.Text}\\{trName}"))
                {
                    trArrayStr += $"{trName} ";

                }

            }
            if (string.IsNullOrEmpty(trArrayStr))
            {
                MessageBox.Show("没有检查到有任何tr文件", "生成shape文件");
                return;
            }

            string command = $"shapeclustering -F font_properties -U unicharset {trArrayStr.TrimEnd()}";

            var filePath = "proShape.bat";
            ExeBat(command, filePath);
            //File.WriteAllText(filePath, command);
            //var pro = Process.Start(filePath);
            //pro.WaitForExit();

            //var cmdResult = Utility.ExecCMD(command);
            //Console.WriteLine(cmdResult);

            this.lv_trainResult.Items.Add("shapetable");
            //this.lv_trainResult.Items.Add("pffmtable");
            MessageBox.Show("生成shape文件完成", "shapeclustering -F font_properties -U unicharset ");
        }

        private void Mftraining(SelectedListViewItemCollection nodes)
        {
            string trArrayStr = "";
            foreach (ListViewItem item in nodes)
            {
                string trName = $"{ Path.GetFileNameWithoutExtension(item.Text)}.tr";
                if (File.Exists($"{this.tb_samplePath.Text}\\{trName}"))
                {
                    trArrayStr += $"{trName} ";

                }

            }
            if (string.IsNullOrEmpty(trArrayStr))
            {
                MessageBox.Show("没有检查到有任何tr文件", "生成聚集字符特征文件");
                return;
            }

            string command = $"mftraining -F font_properties -U unicharset -O unicharset {trArrayStr.TrimEnd()}";

            var filePath = "proFont_properties.bat";
            ExeBat(command, filePath);
            //File.WriteAllText(filePath, command);
            //var pro = Process.Start(filePath);
            //pro.WaitForExit();

            //var cmdResult = Utility.ExecCMD(command);
            //Console.WriteLine(cmdResult);

            this.lv_trainResult.Items.Add("inttemp");
            this.lv_trainResult.Items.Add("pffmtable");

            MessageBox.Show("生成聚集字符特征文件 完成", "Mftraining ");
        }

        private void Cntraining(SelectedListViewItemCollection nodes)
        {
            string trArrayStr = "";
            foreach (ListViewItem item in nodes)
            {
                string trName = $"{ Path.GetFileNameWithoutExtension(item.Text)}.tr";
                if (File.Exists($"{this.tb_samplePath.Text}\\{trName}"))
                {
                    trArrayStr += $"{trName} ";

                }

            }
            if (string.IsNullOrEmpty(trArrayStr))
            {
                MessageBox.Show("没有检查到有任何tr文件", "合并所有tr文件，cntraining");
                return;
            }

            string command = $"cntraining {trArrayStr.TrimEnd()}";

            var filePath = "proCntraining.bat";
            ExeBat(command, filePath);
            //File.WriteAllText(filePath, command);
            //var pro = Process.Start(filePath);
            //pro.WaitForExit();

            //var cmdResult = Utility.ExecCMD(command);
            //Console.WriteLine(cmdResult);

            //this.lv_trainResult.Items.Add("inttemp");

            MessageBox.Show("合并所有tr文件", "Cntraining ");
        }

        private void ReName()
        {
            var nameArray = new string[]{ "unicharset", "shapetable", "pffmtable", "normproto", "inttemp" };
            foreach (var item in nameArray)
            {
                var filePath = $"{this.tb_samplePath.Text}\\{item}";
                var newFilePath = $"{this.tb_samplePath.Text}\\{this.tb_trainLibName.Text}.{item}";
                if (File.Exists(filePath))
                {
                    File.Copy(filePath, newFilePath, true);
                }
                
                
               
            }
        }

        private void CombineTessdata()
        {
            string command = $"combine_tessdata {this.tb_trainLibName.Text}.";
            string cmdPath = "proCombine_tessdata.bat";
            ExeBat(command, cmdPath);
            //var cmd = "proCombine_tessdata.bat";
            //File.WriteAllText(cmd, command);
            //var pro = Process.Start(cmdPath);
            //pro.WaitForExit();
            //Utility.ExecCMD(command);

            var filePath = $"{this.tb_samplePath.Text}\\{this.tb_trainLibName.Text}.traineddata";
            if (File.Exists(filePath))
            {
                string newFilePath = $"{this.tb_tessPath.Text}\\tessdata\\{this.tb_trainLibName.Text}.traineddata";
                File.Copy(filePath, newFilePath, true);
            }
            else
            {
                MessageBox.Show("未获取到训练完成文件");
            }
        }
    }

    public class InitialData
    {
        public string TesseractPath { get; set; }
        public string SamplePath { get; set; }

        public string TrainingLibName { get; set; }
    }
}
