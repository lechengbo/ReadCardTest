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
        public FixedPoint FixedPoint { get; set; } = new FixedPoint();
        public List<OptionArea> OptionAreaList { get; set; } = new List<OptionArea>();

        public Paper NewPaperByOffset(int offsetX,int offsetY)
        {
            var tmpPaper = new Paper()
            {
                FixedPoint = this.FixedPoint.NewFixedPointByOffset(offsetX, offsetY),
            };

            this.OptionAreaList.ForEach(o =>
            {
                tmpPaper.OptionAreaList.Add(o.NewByOffset(offsetX, offsetY));
            });

            return tmpPaper;
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
    }
}
