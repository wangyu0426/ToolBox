#region

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;

#endregion

namespace ToolBox {
    public class RandomImageGenerator : IDisposable {

        private string _code = "NULL";
        private int _width = 100;
        private int _height = 35;
        private Rectangle _rect;
        private Bitmap _bitmap;
        private Graphics _g;
        private Random _rand = new Random();
        private const int _codeLen = 4;
        private const int _paddingX = 5;
        private const int _paddingY = 6;
        private const int _minFontSize = 13;
        private const int _maxFontSize = 15;
        private const int _wordSpacing = 22;
        private const string _fontName = "Verdena";
        private const string _text = "23456789ABCDEFGHJKLMNPQRSTUVWXYZ23456789";
        //1 and I and 0 and O are not used as they can confuse people easily

        //private Color _cBack = Color.White;
        //private Color _cPattern = Color.FromArgb(70, 70, 70);
        //private Color _cText = Color.Blue;
        //private Color _cLines = Color.Green;

        private Color _cBack = Color.DimGray;
        private Color _cPattern = Color.DimGray;
        private Color _cText = Color.White;
        private Color _cLines = Color.DimGray;
        private bool _UseRandomColours = false;
        private bool _DrawRandomPatterns = false;
        private bool _DrawRandomLines = false;

        public void Dispose() {
            GC.SuppressFinalize(this);
            Dispose(true);
        }
        protected virtual void Dispose(bool disposing) {
            if (disposing)
                _bitmap.Dispose();
            _g.Dispose();
        }
        //make a 4 characters random code that we are going to use to draw the image
        public string GetRandomCode() {
            var code = new StringBuilder();
            var alphabets = new List<char>();
            alphabets.AddRange(_text.ToArray());
            for (var i = 0; i < _codeLen; i++) {
                var x = alphabets[_rand.Next(alphabets.Count)];
                alphabets.Remove(x);
                code.Append(x);
            }
            _code = code.ToString();
            return _code;
        }
        //draws an 100 x 25 image, based on the GetRandomText() you have got
        public Bitmap CreateRandomImage() {
            _rect = new Rectangle(0, 0, _width, _height);
            _bitmap = new Bitmap(_width, _height, PixelFormat.Format32bppArgb);
            _g = Graphics.FromImage(_bitmap);
            _g.SmoothingMode = SmoothingMode.AntiAlias;
            if (_UseRandomColours) SetRandomColours();
            var pen = new Pen(_cBack);
            _g.DrawRectangle(pen, _rect);
            _g.FillRectangle(new SolidBrush(_cBack), _rect);
            if (_DrawRandomPatterns) DrawRandomPatterns();
            if (_DrawRandomLines) DrawRandomLines();
            int counter = 0;
            for (int i = 0; i < _code.Length; i++) {
                _g.DrawString(_code[i].ToString(),
                              new Font(_fontName, _rand.Next(_minFontSize, _maxFontSize), GetRandomFontStyle()),
                              new SolidBrush(_cText),
                              new PointF(_paddingX + counter, _paddingY));
                counter += _wordSpacing;
            }

            return _bitmap;
        }
        private void SetRandomColours() {
            var r = _rand.Next(1, 5);
            r = _rand.Next(1, 5);
            if (r == 1) _cPattern = Color.DimGray;
            if (r == 2) _cPattern = Color.CornflowerBlue;
            if (r == 3) _cPattern = Color.Khaki;
            if (r == 4) _cPattern = Color.SandyBrown;
            if (r == 5) _cPattern = Color.Bisque;
            r = _rand.Next(1, 5);
            if (r == 1) _cLines = Color.YellowGreen;
            if (r == 2) _cLines = Color.ForestGreen;
            if (r == 3) _cLines = Color.OrangeRed;
            if (r == 4) _cLines = Color.DeepPink;
            if (r == 5) _cLines = Color.Navy;
            r = _rand.Next(1, 5);
            if (r == 1) _cText = Color.MidnightBlue;
            if (r == 2) _cText = Color.Maroon;
            if (r == 3) _cText = Color.FromArgb(100, 80, 50);
            if (r == 4) _cText = Color.FromArgb(70, 70, 70);
            if (r == 4) _cText = Color.FromArgb(70, 70, 117);
        }
        private void DrawRandomPatterns() {

            var hatchBrush = new HatchBrush(GetRandomEffect(), _cPattern, _cBack);
            _g.FillRectangle(hatchBrush, _rect);
        }
        private void DrawRandomLines() {
            _g.DrawCurve(new Pen(_cLines, 1), GetRandomPointsLong());
            _g.DrawLines(new Pen(_cLines, 1), GetRandomPointsLong());

        }
        private Point[] GetRandomPointsLong() {
            return new List<Point>() {
                new Point(_rand.Next(_paddingX, _width - _paddingX), _rand.Next(_height - _paddingY)),
                new Point(_rand.Next(_paddingX, _width - _paddingX), _rand.Next(_height - _paddingY)),
                new Point(_rand.Next(_paddingX, _width - _paddingX), _rand.Next(_height - _paddingY)),
                new Point(_rand.Next(_paddingX, _width - _paddingX), _rand.Next(_height - _paddingY)),
                new Point(_rand.Next(_paddingX, _width - _paddingX), _rand.Next(_height - _paddingY))
            }.ToArray();
        }
        private HatchStyle GetRandomEffect() {
            var r = _rand.Next(1, 7);
            if (r == 1) return HatchStyle.LightDownwardDiagonal;
            if (r == 2) return HatchStyle.LightUpwardDiagonal;
            if (r == 3) return HatchStyle.DiagonalCross;
            if (r == 4) return HatchStyle.LargeConfetti;
            if (r == 5) return HatchStyle.DottedGrid;
            if (r == 6) return HatchStyle.ZigZag;
            if (r == 7) return HatchStyle.Cross;
            return HatchStyle.Cross;
        }
        private FontStyle GetRandomFontStyle() {
            var r = _rand.Next(1, 3);
            if (r == 1) return FontStyle.Bold;
            if (r == 2) return FontStyle.Italic;
            if (r == 3) return FontStyle.Regular;
            return FontStyle.Regular;
        }
    }
}
