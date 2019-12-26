using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmguTest.Aggregation
{
    public class Paper
    {
        public Size Size { get; set; }
        public string TemplatePath { get; set; }
        public FixedPoint FixedPoint { get; set; } = new FixedPoint();
        public List<OptionArea> OptionAreaList { get; set; } = new List<OptionArea>();

        public List<OffsetArea> OffsetAreas { get; set; } = new List<OffsetArea>();

        public Paper NewPaperByOffset(int offsetX,int offsetY)
        {
            var tmpPaper = new Paper()
            {
                FixedPoint = this.FixedPoint.NewFixedPointByOffset(offsetX, offsetY),
            };
            //选项区域
            this.OptionAreaList.ForEach(o =>
            {
                tmpPaper.OptionAreaList.Add(o.NewByOffset(offsetX, offsetY));
            });
            //偏移量区域
            this.OffsetAreas.ForEach(offset =>
            {
                tmpPaper.OffsetAreas.Add(offset.NewByOffset(offsetX, offsetY));
            });

            return tmpPaper;
        }
        public Paper NewPaperByOffsetS(List<OffsetArea> newOffsetAreaList)
        {
            

            var paper = new Paper();

            //列偏移模块
            var newColOffsetList = new List<Rectangle>();//相对0,0
            var originalColOffsetList = new List<Rectangle>();//相对0,0
            //行偏移模块
            var newRowOffsetList = new List<Rectangle>();//相对0,0
            var originalRowOffsetList = new List<Rectangle>();//相对0,0

            newOffsetAreaList.ForEach(a =>
            {
                if(a.OffsetType== OffsetType.Column)
                {
                    newColOffsetList.AddRange(CommonUse.MoveRects( a.OffsetList,a.Area.X,a.Area.Y));
                }
                else
                {
                    newRowOffsetList.AddRange(CommonUse.MoveRects(a.OffsetList, a.Area.X, a.Area.Y));
                }
            });
            //tmpColOffsetList = tmpColOffsetList.OrderBy(r => r.Y).ToList();
            this.OffsetAreas.ForEach(a =>
            {
                if (a.OffsetType == OffsetType.Column)
                {
                    originalColOffsetList.AddRange(CommonUse.MoveRects( a.OffsetList,a.Area.X,a.Area.Y));
                }
                else
                {
                    originalRowOffsetList.AddRange(CommonUse.MoveRects(a.OffsetList, a.Area.X, a.Area.Y));
                }
            });
            //originalColOffsetList = originalColOffsetList.OrderBy(r => r.Y).ToList();
            var offsetInfoList = OffsetInfo.NewListByColumn(originalColOffsetList, newColOffsetList);
            offsetInfoList.AddRange(OffsetInfo.NewListByRow(originalRowOffsetList, newRowOffsetList));

            //定位点 暂不处理
            paper.FixedPoint = this.FixedPoint.NewFixedPointByOffset(0,0);
            
            //选项区域
            this.OptionAreaList.ForEach(o =>
            {
                paper.OptionAreaList.Add(o.NewByOffsetS(offsetInfoList));
            });
            //偏移量
            this.OffsetAreas.ForEach(a =>
            {
                if(a.OffsetType== OffsetType.Column)
                {
                    var tmpList = OffsetInfo.GetRectsByAreaAndCol(newColOffsetList, a.Area);
                    paper.OffsetAreas.Add(new OffsetArea(a.Area, tmpList, a.OffsetType));
                }
                else
                {
                    var tmpList = OffsetInfo.GetRectsByAreaAndRow(newRowOffsetList, a.Area);
                    paper.OffsetAreas.Add(new OffsetArea(a.Area, tmpList, a.OffsetType));
                }
               
            });


            return paper;
        }

        public List<Rectangle> GetAllRects()
        {
            var list = new List<Rectangle>();
            //定位点
            list.Add(this.FixedPoint.LeftTop.Outer);
            list.Add(this.FixedPoint.LeftTop.Inner);
            list.Add(this.FixedPoint.RightTop.Outer);
            list.Add(this.FixedPoint.RightTop.Inner);
            list.Add(this.FixedPoint.LeftBottom.Outer);
            list.Add(this.FixedPoint.LeftBottom.Inner);
            list.Add(this.FixedPoint.RightBottom.Outer);
            list.Add(this.FixedPoint.RightBottom.Inner);
            //偏移块
            this.OffsetAreas.ForEach(a =>
            {
                list.Add(a.Area);
                list.AddRange(CommonUse.MoveRects(a.OffsetList,a.Area.X,a.Area.Y));
            });
            //选项区域
            this.OptionAreaList.ForEach(a =>
            {
                list.Add(a.Area);
                foreach (var item in a.Options.Values)
                {
                    list.AddRange(CommonUse.MoveRects( item,a.Area.X,a.Area.Y));
                }
            });
            return list;
        }

        public Bitmap GetBitmap(Bitmap bitmap)
        {
            if (!this.Size.IsEmpty)
            {
                var image = new Image<Bgr, byte>(bitmap);
                image = image.Resize(this.Size.Width, this.Size.Height, Emgu.CV.CvEnum.Inter.Linear, false);
                bitmap = image.Bitmap;
            }

            return bitmap;
        }


    }

    
    public class FixedPoint
    {
        public FixedPointDetail LeftTop { get; set; }
        public FixedPointDetail RightTop { get; set; }

        public FixedPointDetail LeftBottom { get; set; }
        public FixedPointDetail RightBottom { get; set; }

        public FixedPoint NewFixedPointByOffset(int offsetX,int offsetY)
        {
            return new FixedPoint()
            {
                LeftTop=this.LeftTop.NewFixedPointDetailByOffset(offsetX,offsetY),
                RightTop = this.RightTop.NewFixedPointDetailByOffset(offsetX, offsetY),
                LeftBottom = this.LeftBottom.NewFixedPointDetailByOffset(offsetX, offsetY),
                RightBottom = this.RightBottom.NewFixedPointDetailByOffset(offsetX, offsetY),
            };
        }

        /// <summary>
        /// 获取定位点的四个顺时针起始点
        /// </summary>
        /// <returns></returns>
        public List<PointF> GetPointsByClockwise()
        {
            return new List<PointF>()
            {
                new PointF(LeftTop.Inner.X,LeftTop.Inner.Y),
                new PointF(RightTop.Inner.X,RightTop.Inner.Y),
                new PointF(RightBottom.Inner.X,RightBottom.Inner.Y),
                new PointF(LeftBottom.Inner.X,LeftBottom.Inner.Y),
                
            };
        }


    }
    public class FixedPointDetail
    {
        public Rectangle Outer { get; set; }
        public Rectangle Inner { get; set; }

        public Rectangle GetEnlargeOuter()
        {
            int offsetX = this.Inner.Width;
            int offsetY = (int)(this.Inner.Height * 1.5);

            var newRect = new Rectangle()
            {
                X = Math.Max(0, this.Outer.X - offsetX),
                Y = Math.Max(0, this.Outer.Y - offsetY),
                Width = this.Outer.Width + offsetX * 2,
                Height = this.Outer.Height + offsetY * 2,
            };
            return newRect;
        }

        public FixedPointDetail NewFixedPointDetailByOffset(int offsetX,int offsetY)
        {
            return new FixedPointDetail()
            {
                Outer = new Rectangle(new Point(this.Outer.X + offsetX, this.Outer.Y + offsetY), this.Outer.Size),
                Inner = new Rectangle(new Point(this.Inner.X + offsetX, this.Inner.Y + offsetY), this.Inner.Size)
            };
        }

       
    }

    public class OptionArea
    {
        public Dictionary<int,List<Rectangle>> Options { get; set; }
        public Rectangle Area { get; set; }
        public int WidthInterval { get; set; }
        public int HeightInterval { get; set; }

        public OptionArea NewByOffset(int offsetX,int offsetY)
        {
            return new OptionArea()
            {
                Options = this.Options,
                Area = new Rectangle(new Point(this.Area.X + offsetX, this.Area.Y + offsetY), this.Area.Size),
                WidthInterval = this.WidthInterval,
                HeightInterval = this.HeightInterval
            };
        }

        public OptionArea NewByOffsetS(List<OffsetInfo> offsetInfos)
        {
            var newOptionArea = new OptionArea() {
                Options = this.Options,
                WidthInterval = this.WidthInterval,
                HeightInterval = this.HeightInterval };
            var offsetCol = OffsetInfo.GetNearestByColumn(offsetInfos, this.Area);
            var offsetRow = OffsetInfo.GetNearestByRow(offsetInfos, this.Area);
            int xAvgCount = 0, yavgCount = 0;
            xAvgCount += offsetCol.offsetX == 0 ? 0 : 1;
            xAvgCount += offsetRow.offsetX == 0 ? 0 : 1;
            yavgCount += offsetCol.offsetY == 0 ? 0 : 1;
            yavgCount += offsetRow.offsetY == 0 ? 0 : 1;

            newOptionArea.Area = CommonUse.MoveRect(this.Area, (offsetCol.offsetX+offsetRow.offsetX), (offsetCol.offsetY+offsetRow.offsetY));

            return newOptionArea;
        }
    }

    /// <summary>
    /// 偏移量区域，规范，最高线和最低线距离最近的偏移测量点大于1.5个偏移高度
    /// 左右距离需要大于0.6个偏移点宽度
    /// 横向的,上下0.4个宽度，左右大于0.8个高度度即可
    /// </summary>
    public class OffsetArea
    {
        protected OffsetArea() {
        }
        public OffsetArea(Rectangle area,List<Rectangle> offsetList, OffsetType offsetType= OffsetType.Column)
        {
            if(area==Rectangle.Empty || offsetList?.Count == 0)
            {
                throw new Exception("偏移点不能为空");
            }
            
            if(offsetType== OffsetType.Column)
            {
                offsetList = offsetList.OrderBy(r => r.Y).ToList();
            }
            else
            {
                offsetList = offsetList.OrderBy(r => r.X).ToList();
            }

            if (!Validate(area, offsetList, offsetType))
            {
                //throw new Exception("偏移区域离偏移块太近");
            }

            this.Area = area;
            this.OffsetList = offsetList;
            this.OffsetType = offsetType;

        }
        public OffsetType OffsetType { get; set; }
        public Rectangle Area { get; set; }

        public List<Rectangle> OffsetList { get; set; }

        public OffsetArea NewByOffset(int offsetX,int offsetY)
        {
            return new OffsetArea(
                CommonUse.MoveRect(this.Area, offsetX, offsetY), 
                this.OffsetList,
                this.OffsetType);
        }

        public static bool Validate(Rectangle area, List<Rectangle> offsetList, OffsetType offsetType = OffsetType.Column)
        {
            bool isValidate = true;

            if(offsetType== OffsetType.Column)
            {
                isValidate = ValidateColumn(area, offsetList);
            }
            else
            {
                isValidate = ValidateRow(area, offsetList);
            }

            return isValidate;
        }

        private static bool ValidateColumn(Rectangle area, List<Rectangle> offsetList) {
            /// 偏移量区域，规范，最高线和最低线距离最近的偏移测量点大于1.5个偏移高度
            /// 左右距离需要大于0.6个偏移点宽度
            bool isValidate = true;

            
            var averageWidth = offsetList.Average(r => r.Width);
            var averageHeight = offsetList.Average(r => r.Height);
            var maxRect = CommonUse.GetMaxRect(offsetList);

            if (averageWidth * 0.6 > maxRect.X || area.Width - averageWidth * 0.6 < maxRect.X+maxRect.Width
                || averageHeight * 1.5 > maxRect.Y  || area.Height< maxRect.Y+maxRect.Height+ averageHeight * 1.5)
            {
                return false;
            }


            return isValidate;
        }
        private static bool ValidateRow(Rectangle area, List<Rectangle> offsetList)
        {
            //横向的,上下0.4个宽度，左右大于0.8个高度度即可
            bool isValidate = true;

            var averageWidth = offsetList.Average(r => r.Width);
            var averageHeight = offsetList.Average(r => r.Height);
            var maxRect = CommonUse.GetMaxRect(offsetList);


            if (maxRect.X< 0.4*averageWidth || maxRect.X+ maxRect.Width+ 0.4 * averageWidth>area.Width
                || maxRect.Y< 0.8 * averageHeight || maxRect.Y+ maxRect.Height+ 0.8 * averageHeight > area.Height)
            {
                return false;
            }



            return isValidate;
        }



    }
    
    /// <summary>
    /// 偏移量信息
    /// </summary>
    public class OffsetInfo
    {
        public Rectangle OriginalRect { get; set; }
        public Rectangle NewRect { get; set; }
        public OffsetType OffsetType { get; set; } = OffsetType.Column;

        public int GetWidthOffset()
        {
            return NewRect.X - OriginalRect.X;
        }
        public int GetHeightOffset()
        {
            return NewRect.Y - OriginalRect.Y;
        }

        public static (int offsetX,int offsetY) GetNearestByColumn(List<OffsetInfo> offsetInfos, Rectangle rect)
        {
            offsetInfos = offsetInfos.Where(o => o.OffsetType == OffsetType.Column).ToList();
            if (offsetInfos?.Count == 0)
            {
                return (0, 0);
            }

            var nearestOffsetInfo = offsetInfos[0];
            var referenceY = rect.Y + rect.Height / 2;
            int nearestDistance = Math.Abs(nearestOffsetInfo.OriginalRect.Y - referenceY);
            for (int i = 1; i < offsetInfos.Count; i++)
            {
                var tmpOffsetInfo = offsetInfos[i];
                int currentDistance = Math.Abs(tmpOffsetInfo.OriginalRect.Y - referenceY);
                if (currentDistance > nearestDistance)
                {
                    break;
                }

                nearestDistance = currentDistance;
                nearestOffsetInfo = tmpOffsetInfo;
            }
            return (nearestOffsetInfo.GetWidthOffset(), nearestOffsetInfo.GetHeightOffset());
        }
        public static (int offsetX, int offsetY) GetNearestByRow(List<OffsetInfo> offsetInfos, Rectangle rect)
        {
            offsetInfos = offsetInfos.Where(o => o.OffsetType == OffsetType.Rows).ToList();
            if (offsetInfos?.Count == 0)
            {
                return (0, 0);
            }

            var nearestOffsetInfo = offsetInfos[0];
            var referenceX = rect.X + rect.Width / 2;

            int nearestDistance = Math.Abs(nearestOffsetInfo.OriginalRect.X - referenceX);
            for (int i = 1; i < offsetInfos.Count; i++)
            {
                var tmpOffsetInfo = offsetInfos[i];
                int currentDistance = Math.Abs(tmpOffsetInfo.OriginalRect.X - referenceX);
                if (currentDistance > nearestDistance)
                {
                    break;
                }

                nearestDistance = currentDistance;
                nearestOffsetInfo = tmpOffsetInfo;
            }
            return (nearestOffsetInfo.GetWidthOffset(), nearestOffsetInfo.GetHeightOffset());
        }

        public static (int offsetX,int offsetY) GetNearest(List<OffsetInfo> offsetInfos, Rectangle rect)
        {
            var rowOffset = GetNearestByRow(offsetInfos, rect);
            var colOffset = GetNearestByColumn(offsetInfos, rect);
            return (rowOffset.offsetX + colOffset.offsetX, rowOffset.offsetY + colOffset.offsetY);
        }

        public static List<OffsetInfo> NewListByColumn(List<Rectangle> originalList,List<Rectangle> newList)
        {
            if(originalList?.Count != newList?.Count || originalList?.Count==0)
            {
                throw new Exception("原始偏移量识别块和现有的偏移量数量不同");
            }
            var list = new List<OffsetInfo>();
            originalList = originalList.OrderBy(r => r.Y).ToList();
            newList = newList.OrderBy(r => r.Y).ToList();
            for (int i = 0; i < originalList.Count; i++)
            {
                list.Add(new OffsetInfo() { OriginalRect = originalList[i], NewRect = newList[i] });
            }

            return list;
        }
        public static List<OffsetInfo> NewListByRow(List<Rectangle> originalList, List<Rectangle> newList)
        {
            if (originalList?.Count != newList?.Count || originalList?.Count == 0)
            {
                throw new Exception("原始偏移量识别块和现有的偏移量数量不同");
            }
            var list = new List<OffsetInfo>();
            originalList = originalList.OrderBy(r => r.X).ToList();
            newList = newList.OrderBy(r => r.X).ToList();
            for (int i = 0; i < originalList.Count; i++)
            {
                list.Add(new OffsetInfo() { OriginalRect = originalList[i], NewRect = newList[i], OffsetType= OffsetType.Rows });
            }

            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rectList">相对图片0,0</param>
        /// <param name="area"></param>
        /// <returns></returns>
        public static List<Rectangle> GetRectsByAreaAndCol(List<Rectangle> rectList,Rectangle area)
        {
            var list = new List<Rectangle>();
            rectList.ForEach(r =>
            {
                if(r.Y>area.Y && r.Y< area.Y + area.Height)
                {
                    list.Add(CommonUse.MoveRect( r,-area.X,-area.Y));
                }
            });

            return list;
        }
        public static List<Rectangle> GetRectsByAreaAndRow(List<Rectangle> rectList, Rectangle area)
        {
            var list = new List<Rectangle>();
            rectList.ForEach(r =>
            {
                if (r.X > area.X && r.X < area.X + area.Width)
                {
                    list.Add(CommonUse.MoveRect(r, -area.X, -area.Y));
                }
            });

            return list;
        }
    }

    public enum OffsetType
    {
        /// <summary>
        /// 以列的形式排列的定位点
        /// </summary>
        Column,
        /// <summary>
        /// 以行的形式排列的定位点
        /// </summary>
        Rows,
        
    }
}
