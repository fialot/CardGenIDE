using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.ComponentModel;
using System.Xml;
using System.IO;
using System.Reflection;

using Svg;
using myFunctions;
using WordEditor;
using System.Windows.Forms;
using System.Runtime.Serialization.Formatters.Binary;

public enum CardObjectType { label, text, image, shape, curve }

namespace CardGen
{
    #region Enum

    public enum CardSide { top, back }

    public enum Align
    {
        left,           
        center,            
        right,    
    }

    public enum Shape
    {
        arc,
        rectangle,
        ellipse,
        pie
    }

    public enum Curve
    {
        line,
        curve,
        bezierCurve,
        polygon
    }

    public enum CardUnits
    {
        px,             // pixels
        mm,             // mm
        percentage,     // percentage of card
    }

    public enum CardImgUnits
    {
        px,             // pixels
        mm,             // mm
        percentage,     // percentage of card
        imgPerc         // percentage of input image
    }

    public enum GenType
    {
        png,
        docx
    }
    
    #endregion

    #region Structures

    public struct TextLocation
    {
        public float x; // mm
        public float y; // mm
        public float width; // mm
        public float height; // mm
        public string text;
        public int angle;
        public Align alignment;
        public Color color;
        public System.Drawing.Font font;
    }

    public struct TIDVal
    {
        public string ID;
        public string value;
    }

    [TypeConverter(typeof(ValueTypeTypeConverter))]
    public struct TPointValue
    {
        public int Value  { get; set; }
        public CardUnits Unit { get; set; }

        public TPointValue(int value, CardUnits unit)
        {
            Value = value;
            Unit = unit;
        }
    }

    public class ValueTypeTypeConverter : System.ComponentModel.ExpandableObjectConverter
    {
        public override bool GetCreateInstanceSupported(System.ComponentModel.ITypeDescriptorContext context)
        {
            return true;
        }

        public override object CreateInstance(System.ComponentModel.ITypeDescriptorContext context, System.Collections.IDictionary propertyValues)
        {
            if (propertyValues == null)
                throw new ArgumentNullException("propertyValues");

            object boxed = Activator.CreateInstance(context.PropertyDescriptor.PropertyType);
            foreach (System.Collections.DictionaryEntry entry in propertyValues)
            {
                System.Reflection.PropertyInfo pi = context.PropertyDescriptor.PropertyType.GetProperty(entry.Key.ToString());
                if ((pi != null) && (pi.CanWrite))
                {
                    pi.SetValue(boxed, Convert.ChangeType(entry.Value, pi.PropertyType), null);
                }
            }
            return boxed;
        }
    }

    [TypeConverter(typeof(ValueTypeTypeConverter))]
    public struct TImgPointValue
    {
        public int Value { get; set; }
        public CardImgUnits Unit { get; set; }

        public TImgPointValue(int value, CardImgUnits unit)
        {
            Value = value;
            Unit = unit;
        }
    }

    [TypeConverter(typeof(ValueTypeTypeConverter))]
    public struct TPoint
    {
        public TPointValue x { get; set; } 
        public TPointValue y { get; set; } 

        public TPoint(int xVal, int yVal)
        {
            x = new TPointValue(xVal, CardUnits.percentage);
            y = new TPointValue(yVal, CardUnits.percentage);
        }

        public TPoint(TPointValue xVal, TPointValue yVal)
        {
            x = new TPointValue(xVal.Value, xVal.Unit);
            y = new TPointValue(yVal.Value, xVal.Unit);
        }
    }

    [TypeConverter(typeof(ValueTypeTypeConverter))]
    public struct TImgPoint
    {
        public TImgPointValue x { get; set; }
        public TImgPointValue y { get; set; }

        public TImgPoint(int xVal, int yVal)
        {
            x = new TImgPointValue(xVal, CardImgUnits.percentage);
            y = new TImgPointValue(yVal, CardImgUnits.percentage);
        }

        public TImgPoint(TImgPointValue xVal, TImgPointValue yVal)
        {
            x = new TImgPointValue(xVal.Value, xVal.Unit);
            y = new TImgPointValue(yVal.Value, xVal.Unit);
        }
    }

    public struct TCardObject
    {
        public CardObjectType type;
        public int index;
    }

    public struct TCardSet
    {
        public int width;
        public int height;
        public int dpi;

    }

    public struct TGenSet
    {
        public bool genBackCards;
        public bool genTextInDoc;
        public GenType genType;
        public PageSizes genPaper;
        public bool genPapLandskape;
    }

    public class TCardLabel
    {
        public string text { get; set; }
        public string ID { get; set; }
        public string type { get; set; }
        //[Description(""), Category("Format")]
        //public int size { get; set; }
        [Description(""), Category("Format")]
        public Font font { get; set; }
        [Description(""), Category("Format")]
        public bool capitalize { get; set; }
        [Description(""), Category("Format")]
        public Color color { get; set; }
        [Description(""), Category("Format")]
        public Color backColor { get; set; }
        [Description(""), Category("Format")]
        public Align alignment { get; set; }
        [Description(""), Category("Layout")]
        public TPoint location { get; set; }
        [Description(""), Category("Layout")]
        public int angle { get; set; }
        [Description(""), Category("View")]
        public bool show { get; set; }
        public CardSide side;

    }

    public class TCardText
    {
        public string text { get; set; }
        public string ID { get; set; }
        public string type { get; set; }
        [Description(""), Category("Format")]
        public Font font { get; set; }
        [Description(""), Category("Format")]
        public Color color { get; set; }
        [Description(""), Category("Format")]
        public Color backColor { get; set; }
        [Description(""), Category("Format")]
        public Align alignment { get; set; }
        [Description(""), Category("Layout")]
        public Align alignHeight { get; set; }
        [Description(""), Category("Layout")]
        public TPoint location { get; set; }
        [Description(""), Category("Layout")]
        public TPoint size { get; set; }
        [Description(""), Category("Layout")]
        public int angle { get; set; }
        [Description(""), Category("View")]
        public bool show { get; set; }
        public CardSide side;
    }

    public class TCardImg
    {
        public string path { get; set; }
        public string ID { get; set; }
        public string type { get; set; }
        [Description(""), Category("Layout")]
        public Align alignHeight { get; set; }
        [Description(""), Category("Layout")]
        public Align alignment { get; set; }
        [Description(""), Category("Layout")]
        public TPoint location { get; set; }
        [Description(""), Category("Layout")]
        public TImgPoint size { get; set; }
        [Description(""), Category("Layout")]
        public int angle { get; set; }
        [Description(""), Category("View")]
        public bool show { get; set; }
        public CardSide side;
    }

    public class TCardShape
    {
        public string ID { get; set; }
        public string type { get; set; }
        
        public Shape shape { get; set; }

        public Color color { get; set; }
        public Color fillColor { get; set; }
        public int penSize { get; set; }

        
        public TPointValue round{ get; set; }
        [Description(""), Category("Layout")]

        public TPoint location { get; set; }
        public TPoint size { get; set; }

        //public TPoint[] points { get; set; }
        [Description(""), Category("Layout")]
        public Align alignment { get; set; }
        [Description(""), Category("Layout")]
        public Align alignHeight { get; set; }
        [Description(""), Category("Layout")]
        public int angle { get; set; }
        [Description(""), Category("Layout")]
        public int startAngle { get; set; }
        [Description(""), Category("Layout")]
        public int sweepAngle { get; set; }
        

        [Description(""), Category("View")]
        public bool show { get; set; }
    }

    public class TCardCurve
    {
        public string ID { get; set; }
        public string type { get; set; }

        public Curve curve { get; set; }

        public Color color { get; set; }
        public Color fillColor { get; set; }
        public int penSize { get; set; }


        [Description(""), Category("Layout")]
        public TPoint[] points { get; set; }
        [Description(""), Category("Layout")]
        public Align alignment { get; set; }
        [Description(""), Category("Layout")]
        public Align alignHeight { get; set; }
        [Description(""), Category("Layout")]
        public int angle { get; set; }
        [Description(""), Category("Layout")]
        public int endAngle { get; set; }


        [Description(""), Category("View")]
        public bool show { get; set; }
    }

    #endregion

    public class CardSideTemplate
    {
        public List<TCardObject> itemList;

        public List<TCardText> textList;
        public List<TCardLabel> labelList;
        public List<TCardImg> imgList;
        public List<TCardShape> shapeList;
        public List<TCardCurve> curveList;

        int Width;
        int Height;
        int DPI;

        public string WorkPath { get; set; }

        CardImg Image;

        public CardSideTemplate(int width, int height, int dpi)
        {
            Width = width;
            Height = height;
            DPI = dpi;
            itemList = new List<TCardObject>();
            labelList = new List<TCardLabel>();
            textList = new List<TCardText>();
            imgList = new List<TCardImg>();
            shapeList = new List<TCardShape>();
            curveList = new List<TCardCurve>();
            Image = new CardImg(Width, Height, DPI);
        }

        public void SetSize(int width, int height, int dpi)
        {
            Width = width;
            Height = height;
            DPI = dpi;
            Image = new CardImg(Width, Height, DPI);
        }

        public int AddItem(TCardObject item)
        {
            int num = 0;
            switch (item.type)
            {
                case CardObjectType.curve:
                    num = AddCurve(item.index);
                    break;
                case CardObjectType.image:
                    num = AddImage(item.index);
                    break;
                case CardObjectType.label:
                    num = AddLabel(item.index);
                    break;
                case CardObjectType.shape:
                    num = AddShape(item.index);
                    break;
                case CardObjectType.text:
                    num = AddText(item.index);
                    break;
            }
            return num;
        }

        public int AddLabel(int num = -1)
        {
            TCardLabel labelItem = new TCardLabel();
            if (num >= 0)
            {
                labelItem.text = labelList[num].text;
                labelItem.ID = "label" + (labelList.Count + 1).ToString();
                labelItem.location = labelList[num].location;
                //labelItem.size = 10;
                labelItem.color = labelList[num].color;
                labelItem.backColor = labelList[num].backColor;
                labelItem.font = labelList[num].font;
                labelItem.capitalize = labelList[num].capitalize;
                labelItem.alignment = labelList[num].alignment;
                labelItem.show = labelList[num].show;
            }
            else
            {
                labelItem.text = "";
                labelItem.ID = "label" + (labelList.Count + 1).ToString();
                labelItem.location = new TPoint(0, 0);
                //labelItem.size = 10;
                labelItem.color = Color.Black;
                labelItem.backColor = Color.Transparent;
                labelItem.font = new Font("Arial", 10);
                labelItem.capitalize = false;
                labelItem.alignment = Align.left;
                labelItem.show = true;
            }
            

            return AddLabel(labelItem);
        }

        public int AddLabel(TCardLabel labelItem)
        {
            TCardObject item = new TCardObject();
            
            labelList.Add(labelItem);

            item.type = CardObjectType.label;
            item.index = labelList.Count - 1;
            itemList.Add(item);
            return itemList.Count - 1;
        }

        public int AddText(int num = -1)
        {
            TCardText textItem = new TCardText();
            if (num >= 0)
            {
                textItem.text = textList[num].text;
                textItem.ID = "text" + (textList.Count + 1).ToString();
                textItem.location = textList[num].location;

                textItem.color = textList[num].color;
                textItem.backColor = textList[num].backColor;
                textItem.font = textList[num].font;
                textItem.alignment = textList[num].alignment;
                textItem.alignHeight = textList[num].alignHeight;

                textItem.size = textList[num].size;
                textItem.show = textList[num].show;
            }
            else
            {
                textItem.text = "";
                textItem.ID = "text" + (textList.Count + 1).ToString();
                textItem.location = new TPoint(0, 0);

                textItem.color = Color.Black;
                textItem.backColor = Color.Transparent;
                textItem.font = new Font("Arial", 10);
                textItem.alignment = Align.left;
                textItem.alignHeight = Align.left;

                textItem.size = new TPoint(100, 50);
                textItem.show = true;
            }
            return AddText(textItem);
        }

        public int AddText(TCardText textItem)
        {
            TCardObject item = new TCardObject();

            textList.Add(textItem);

            item.type = CardObjectType.text;
            item.index = textList.Count - 1;
            itemList.Add(item);
            return itemList.Count - 1;
        }

        public int AddImage(int num = -1)
        {
            TCardImg imgItem = new TCardImg();
            if (num >= 0)
            {
                imgItem.ID = "image" + (imgList.Count + 1).ToString();
                imgItem.location = imgList[num].location;
                imgItem.size = imgList[num].size;
                imgItem.alignment = imgList[num].alignment;
                imgItem.alignHeight = imgList[num].alignHeight;
                imgItem.angle = imgList[num].angle;
                imgItem.path = imgList[num].path;
                imgItem.type = imgList[num].type;
                imgItem.show = imgList[num].show;
            } else
            {
                imgItem.ID = "image" + (imgList.Count + 1).ToString();
                imgItem.location = new TPoint(0, 0);
                imgItem.size = new TImgPoint(0, 0);
                imgItem.alignment = Align.left;
                imgItem.path = "";
                imgItem.type = "";
                imgItem.show = true;
            }

            return AddImage(imgItem);
        }

        public int AddImage(TCardImg imgItem)
        {
            TCardObject item = new TCardObject();
            
            imgList.Add(imgItem);

            item.type = CardObjectType.image;
            item.index = imgList.Count - 1;
            itemList.Add(item);
            return itemList.Count - 1;
        }

        public int AddShape(int num = -1)
        {
            TCardShape shapeItem = new TCardShape();
            if (num >= 0)
            {
                shapeItem.ID = "shape" + (shapeList.Count + 1).ToString();
                shapeItem.color = shapeList[num].color;
                shapeItem.penSize = shapeList[num].penSize;
                shapeItem.fillColor = shapeList[num].fillColor;
                shapeItem.alignment = shapeList[num].alignment;
                shapeItem.alignHeight = shapeList[num].alignHeight;
                shapeItem.shape = shapeList[num].shape;
                shapeItem.location = shapeList[num].location;
                shapeItem.size = shapeList[num].size;
                shapeItem.round = shapeList[num].round;
                shapeItem.startAngle = shapeList[num].startAngle;
                shapeItem.sweepAngle = shapeList[num].sweepAngle;
                shapeItem.show = shapeList[num].show;
            } else
            {
                shapeItem.ID = "shape" + (shapeList.Count + 1).ToString();
                shapeItem.color = Color.Black;
                shapeItem.penSize = 2;
                shapeItem.fillColor = Color.Transparent;
                shapeItem.alignment = Align.left;
                shapeItem.alignHeight = Align.left;
                shapeItem.shape = Shape.rectangle;
                shapeItem.location = new TPoint(0, 0);
                shapeItem.size = new TPoint(20, 20);
                shapeItem.round = new TPointValue(0, CardUnits.px);
                shapeItem.startAngle = 0;
                shapeItem.sweepAngle = 90;
                shapeItem.show = true;
            }

            return AddShape(shapeItem);

        }

        public int AddShape(TCardShape shapeItem)
        {
            TCardObject item = new TCardObject();

            shapeList.Add(shapeItem);

            item.type = CardObjectType.shape;
            item.index = shapeList.Count - 1;
            itemList.Add(item);
            return itemList.Count - 1;
        }

        public int AddCurve(int num = -1)
        {
            TCardCurve curveItem = new TCardCurve();
            if (num >= 0)
            {
                curveItem.ID = "curve" + (curveList.Count + 1).ToString();
                curveItem.color = curveList[num].color;
                curveItem.penSize = curveList[num].penSize;
                curveItem.fillColor = curveList[num].fillColor;
                curveItem.alignment = curveList[num].alignment;
                curveItem.alignHeight = curveList[num].alignHeight;
                curveItem.curve = curveList[num].curve;
                curveItem.points = curveList[num].points;
                curveItem.show = curveList[num].show;
            } else
            {
                curveItem.ID = "curve" + (curveList.Count + 1).ToString();
                curveItem.color = Color.Black;
                curveItem.penSize = 2;
                curveItem.fillColor = Color.Transparent;
                curveItem.alignment = Align.left;
                curveItem.alignHeight = Align.left;
                curveItem.curve = Curve.line;
                curveItem.points = new TPoint[2] { new TPoint(0, 0), new TPoint(20, 20) };
                curveItem.show = true;
            }
            return AddCurve(curveItem);

        }

        public int AddCurve(TCardCurve curveItem)
        {
            TCardObject item = new TCardObject();

            curveList.Add(curveItem);

            item.type = CardObjectType.curve;
            item.index = curveList.Count - 1;
            itemList.Add(item);
            return itemList.Count - 1;
        }

        public void InsertItem(int index, TCardObject item)
        {
            if (index < itemList.Count)
            {
                itemList.Insert(index, item);
            } else
            {
                itemList.Add(item);
            }
        }

        public void DelItem(int index)
        {
            if (index < itemList.Count)
            {
                itemList.RemoveAt(index);
            }
        }

        public object GetItem(int index)
        {
            if (itemList[index].type == CardObjectType.label)
            {
                return labelList[itemList[index].index];
            }
            else if (itemList[index].type == CardObjectType.text)
            {
                return textList[itemList[index].index];
            }
            else if (itemList[index].type == CardObjectType.image)
            {
                return imgList[itemList[index].index];
            }
            else if (itemList[index].type == CardObjectType.shape)
            {
                return shapeList[itemList[index].index];
            }
            else if (itemList[index].type == CardObjectType.curve)
            {
                return curveList[itemList[index].index];
            }
            else
                return null;
        }

        public List<string> GetObjectList()
        {
            List<string> list = new List<string>();
            for (int i = 0; i < itemList.Count; i++)
            {
                switch (itemList[i].type)
                {
                    case CardObjectType.label:
                        list.Add(labelList[itemList[i].index].ID + " (label)");
                        break;
                    case CardObjectType.text:
                        list.Add(textList[itemList[i].index].ID + " (text)");
                        break;
                    case CardObjectType.image:
                        list.Add(imgList[itemList[i].index].ID + " (image)");
                        break;
                    case CardObjectType.shape:
                        list.Add(shapeList[itemList[i].index].ID + " (shape)");
                        break;
                    case CardObjectType.curve:
                        list.Add(shapeList[itemList[i].index].ID + " (curve)");
                        break;
                    default:
                        list.Add("(unknown)");
                        break;
                }
            }
            return list;
        }


        public bool ContainType(List<TIDVal> list, string[] type)
        {
            for (int j = 0; j < list.Count; j++)
            {
                if (list[j].ID == "type")
                {
                    string[] typeList = list[j].value.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string item in typeList)
                    {
                        foreach (string typeItem in type)
                        {
                            if (item.Trim() == typeItem.Trim())
                            {
                                return true;
                            }
                        } 
                    }
                }
            }
            return false;
        }

        public string GetValFromID(List<TIDVal> list, string ID)
        {
            for (int j = 0; j < list.Count; j++)
            {
                if (list[j].ID == ID)
                {
                    if (list[j].value == null) return "";
                    return list[j].value.Trim();
                }
            }
            return "";
        }

        public Image GetImage(List<TIDVal> list = null, bool withText = true)
        {
            CardImg card = new CardImg(Width, Height, DPI);
            string text;

            bool showItem;

            

            for (int i = 0; i < itemList.Count; i++)
            {
                Pen pen;
                Brush brush;
                showItem = true;

                switch (itemList[i].type)
                {
                    case CardObjectType.label:
                        if (withText)
                        {
                            TCardLabel cardLabel = labelList[itemList[i].index];
                            text = "<" + cardLabel.ID + ">";
                            if (cardLabel.text != null && cardLabel.text != "") text = cardLabel.text;
                            if (list != null)
                            {
                                for (int j = 0; j < list.Count; j++)
                                {
                                    if (list[j].ID == cardLabel.ID) text = list[j].value;
                                }
                                if (cardLabel.type != null && cardLabel.type != "")
                                {
                                    showItem = ContainType(list, cardLabel.type.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries));
                                }
                            } else
                            {
                                showItem = cardLabel.show;
                            }
                            text = text.Replace(@"\n", "\n");

                            // ----- init settings -----
                            TPoint location = cardLabel.location;
                            int angle = cardLabel.angle;
                            Font font = cardLabel.font;
                            Color color = cardLabel.color;
                            Color backColor = cardLabel.backColor;
                            Align alignment = cardLabel.alignment;
                            // ----- check function settings -----
                            int pos1 = text.IndexOf("{");
                            int pos2 = text.IndexOf("}");
                            string funcText = "";
                            if (pos1 >= 0 && pos2 > pos1)
                            {
                                funcText = text.Substring(pos1 + 1, pos2 - pos1 - 1);
                                text = text.Remove(pos1, pos2 - pos1 + 1);
                                string[] func = funcText.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
                                for (int j = 0; j < func.Length; j++)
                                {
                                    string[] funcItem = func[j].Split(new string[] { "=" }, StringSplitOptions.RemoveEmptyEntries);
                                    if (funcItem.Length == 2)
                                    {
                                        if (funcItem[0] == "color")
                                        {
                                            color = Color.FromName(funcItem[1]);
                                        }
                                    }
                                }

                            }

                            if (cardLabel.capitalize) text = text.ToUpper();
                            if (showItem) card.DrawLabel(text, location, angle, font, color, backColor, alignment);
                        }
                        break;
                    case CardObjectType.text:
                        if (withText)
                        {
                            TCardText cardText = textList[itemList[i].index];
                            text = "<" + cardText.ID + ">";
                            if (cardText.text != null && cardText.text != "") text = cardText.text;
                            if (list != null)
                            {
                                for (int j = 0; j < list.Count; j++)
                                {
                                    if (list[j].ID == cardText.ID) text = list[j].value;
                                }
                                if (cardText.type != null && cardText.type != "")
                                {
                                    showItem = ContainType(list, cardText.type.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries));
                                }
                            }
                            else
                            {
                                showItem = cardText.show;
                            }
                            text = text.Replace(@"\n", "\n");
                            if (showItem) card.DrawText(text, cardText.location, cardText.size, cardText.angle, cardText.font, cardText.color, cardText.backColor, cardText.alignment, cardText.alignHeight);
                            
                        }
                        break;
                    case CardObjectType.image:
                        TCardImg cardImg = imgList[itemList[i].index];
                        string usedPath = "";

                        if (list != null)
                        {
                            usedPath = GetValFromID(list, imgList[itemList[i].index].ID);
                            if (cardImg.type != null && cardImg.type != "")
                            {
                                showItem = ContainType(list, cardImg.type.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries));
                            }
                            else
                            {
                                if (usedPath != "") showItem = true;
                            }
                        }
                        else
                        {
                            showItem = cardImg.show;
                        }

                        string[] pathList = cardImg.path.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
                        
                        foreach (string item in pathList)
                        {
                            if (item.Contains("="))
                            {
                                string typeName = item.Substring(0, item.IndexOf("=")).Trim();
                                string value = item.Substring(item.IndexOf("=") + 1);
                                if (usedPath == "")
                                    usedPath = value.Trim();
                                if (list != null)
                                {
                                    if (ContainType(list, typeName.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries)))
                                    {
                                        usedPath = value.Trim();
                                    }
                                }
                            }
                            else
                            {
                                if (list == null)
                                {
                                    if (item.Trim() != "")
                                        usedPath = item.Trim();
                                }
                                else
                                {
                                    if (usedPath == "" && item.Trim() != "")
                                        usedPath = item.Trim();
                                }
                            }
                        }

                        if (list != null && (usedPath == "")) showItem = false;
                        if (showItem) card.DrawImage(WorkPath + Path.DirectorySeparatorChar + usedPath, cardImg.location, cardImg.size, cardImg.angle, cardImg.alignment, cardImg.alignHeight);
                        break;
                    case CardObjectType.shape:
                        TCardShape cardShape = shapeList[itemList[i].index];

                        pen = new Pen(cardShape.color, cardShape.penSize);
                        brush = new SolidBrush(cardShape.fillColor);
                        if (list != null)
                        {
                            if (cardShape.type != null && cardShape.type != "")
                            {
                                showItem = ContainType(list, cardShape.type.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries));
                            }
                        }
                        else
                        {
                            showItem = cardShape.show;
                        }
                        if (showItem) card.DrawShape(cardShape.shape, pen, brush, cardShape.location, cardShape.size, cardShape.angle, cardShape.startAngle, cardShape.sweepAngle, cardShape.alignment, cardShape.alignHeight, cardShape.round);
                        break;
                    case CardObjectType.curve:
                        TCardCurve cardCurve = curveList[itemList[i].index];

                        pen = new Pen(cardCurve.color, cardCurve.penSize);
                        brush = new SolidBrush(cardCurve.fillColor);

                        if (list != null)
                        {
                            if (cardCurve.type != null && cardCurve.type != "")
                            {
                                showItem = ContainType(list, cardCurve.type.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries));
                            }
                        }
                        else
                        {
                            showItem = cardCurve.show;
                        }
                        //if (showItem) card.DrawShape(cardShape.shape, pen, brush, cardShape.points, cardShape.angle, cardShape.endAngle, cardShape.alignment, cardShape.alignHeight, cardShape.round);
                        break;
                }
            }
            return card.Image();
        }

        public List<TextLocation> GetTextLoc(List<TIDVal> list = null)
        {
            List<TextLocation> txtLoc = new List<TextLocation>();
            

            for (int i = 0; i < itemList.Count; i++)
            {
                TextLocation item = new TextLocation();
                if (itemList[i].type == CardObjectType.label)
                {
                    item.text = labelList[itemList[i].index].text;
                    if (list != null)
                    {
                        for (int j = 0; j < list.Count; j++)
                        {
                            if (list[j].ID == labelList[itemList[i].index].ID) item.text = list[j].value;
                        }
                    }
                    item.font = labelList[itemList[i].index].font;
                    item.alignment = labelList[itemList[i].index].alignment;

                    if (labelList[itemList[i].index].location.x.Unit == CardUnits.percentage)
                    {
                        item.x = (this.Width * labelList[itemList[i].index].location.x.Value) / 100.0f;
                        //item.x = (float)(x / (0.03937 * DPI));
                    } else if (labelList[itemList[i].index].location.x.Unit == CardUnits.px)
                    {
                        item.x = (float)(labelList[itemList[i].index].location.x.Value / (0.03937 * DPI));
                    }
                    else
                    {
                        item.x = labelList[itemList[i].index].location.x.Value;
                    }

                    if (labelList[itemList[i].index].location.y.Unit == CardUnits.percentage)
                    {
                        item.y = (this.Height * labelList[itemList[i].index].location.y.Value) / 100.0f;
                        //item.y = (float)(y / (0.03937 * DPI));
                    }
                    else if (labelList[itemList[i].index].location.y.Unit == CardUnits.px)
                    {
                        item.y = (float)(labelList[itemList[i].index].location.y.Value / (0.03937 * DPI));
                    }
                    else
                    {
                        item.y = labelList[itemList[i].index].location.y.Value;
                    }

                    
                    item.width = this.Width - item.x;
                    item.height = this.Height - item.y;
                    item.color = labelList[itemList[i].index].color;
                    txtLoc.Add(item);
                }

            }

            return txtLoc;
        }
    }

    /// <summary>
    /// Card Deck class
    /// </summary>
    public class CardDeck
    {

        /// <summary>
        /// Section enum for XML deck template loading
        /// </summary>
        enum ESection { none, deck, settings, foreCard, backCard }      // for XML

        /// <summary>
        /// Structure of Word saved picture data
        /// </summary>
        public struct TImgCardResult
        {
            public string ID;       // ForeSide Card Image ID in Word
            public string backID;   // BackSide Card Image ID in Word
            public Int64 width;     // Image size
            public Int64 height;
        }


        /// <summary>
        /// Card deck script filename
        /// </summary>
        public string ScriptFileName { get; private set; }

        /// <summary>
        /// Output folder for Export cards
        /// </summary>
        public string OutputFolder { get; private set; }

        /// <summary>
        /// XML template filename
        /// </summary>
        public string TemplateFileName { get; set; }

        /// <summary>
        /// Data (csv) filename
        /// </summary>
        public string DataFileName { get; set; }

        /// <summary>
        /// Deck data is changed after last saving
        /// </summary>
        public bool IsChanged { get; private set; }

        /// <summary>
        /// Deck work path
        /// </summary>
        string deckFolder;


        


        
        public TCardSet cardSet;
        public TGenSet genSet;

        public CardSideTemplate CardFore;
        public CardSideTemplate CardBack;

        public CardImg card;

        #region Public

        // ----- CONSTRUCTOR -----
        /// <summary>
        /// Card deck Constructor
        /// </summary>
        public CardDeck()
        {
            ScriptFileName = "";                // deck is not saved
            createNewDeck();                            // create new deck with default cardsize
        }

        /// <summary>
        /// Card deck Constructor
        /// </summary>
        /// <param name="width">Card width [mm]</param>
        /// <param name="heigth">Card heigth [mm]</param>
        /// <param name="dpi">Card DPI</param>
        public CardDeck(int width, int heigth, int dpi)
        {
            ScriptFileName = "";            // deck is not saved
            createNewDeck(width, heigth, dpi);      // create new deck with card size
        }

        // ----- LOAD / SAVE -----
        /// <summary>
        /// Load Deck script
        /// </summary>
        /// <param name="fileName"></param>

        public void LoadDeck(string fileName)
        {
            this.ScriptFileName = fileName;
            deckFolder = Path.GetDirectoryName(Path.GetFullPath(fileName));

            createNewDeck();

            string[] lines = Files.LoadFileLines(fileName, true);
            for (int i = 0; i < lines.Length; i++)
            {
                lines[i] = lines[i].Trim().ToLower();
                if (lines[i].IndexOf("template") == 0)
                {
                    lines[i] = lines[i].Remove(0, 8).Trim();
                    TemplateFileName = lines[i];
                    if (Path.GetFullPath(TemplateFileName) != TemplateFileName)
                        TemplateFileName = deckFolder + Path.DirectorySeparatorChar + TemplateFileName;
                    LoadXMLTemplate(TemplateFileName);
                }
                else if (lines[i].IndexOf("cardsize") == 0)
                {
                    lines[i] = lines[i].Remove(0, 8).Trim();
                    string[] val = lines[i].Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                    if (val.Length >= 2)
                    {
                        cardSet.width = Conv.ToIntDef(val[0].Trim(), 60);
                        cardSet.height = Conv.ToIntDef(val[1].Trim(), 90);
                        if (val.Length >= 3)
                        {
                            cardSet.dpi = Conv.ToIntDef(val[2].Trim(), 300);
                        }
                        CardFore.SetSize(cardSet.width, cardSet.height, cardSet.dpi);
                        CardBack.SetSize(cardSet.width, cardSet.height, cardSet.dpi);
                    }
                }
                else if (lines[i].IndexOf("data") == 0)
                {
                    lines[i] = lines[i].Remove(0, 4).Trim();
                    DataFileName = lines[i];
                    try
                    {
                        if (Path.GetFullPath(DataFileName) != DataFileName)
                            DataFileName = deckFolder + Path.DirectorySeparatorChar + DataFileName;
                    }
                    catch (Exception)
                    {
                    }

                }
                else if (lines[i].IndexOf("generate") == 0)
                {
                    lines[i] = lines[i].Remove(0, 8).Trim();
                    string[] val = lines[i].Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                    if (val.Length >= 1)
                    {
                        if (val[0].Trim().ToLower() == "docx")
                        {
                            genSet.genType = GenType.docx;
                        }
                        else
                        {
                            genSet.genType = GenType.png;
                        }

                        if (val.Length >= 2)
                        {
                            if (val[1].Trim() == "1" || val[1].Trim().ToLower() == "true")
                            {
                                genSet.genBackCards = true;
                            }
                            else genSet.genBackCards = false;

                            if (val.Length >= 3)
                            {
                                if (val[2].Trim() == "1" || val[2].Trim().ToLower() == "true")
                                {
                                    genSet.genTextInDoc = true;
                                }
                                else genSet.genTextInDoc = false;
                            }

                            if (val.Length >= 4)
                            {
                                try
                                {
                                    genSet.genPaper = (PageSizes)Enum.Parse(typeof(PageSizes), val[3], true);
                                } catch
                                {
                                    genSet.genPaper = PageSizes.A4;
                                }
                            }

                            if (val.Length >= 5)
                            {
                                if (val[4].Trim() == "1" || val[4].Trim().ToLower() == "true")
                                {
                                    genSet.genPapLandskape = true;
                                }
                                else genSet.genPapLandskape = false;
                            }
                        }
                    }
                }
            }
            IsChanged = false;
        }

        /// <summary>
        /// Save Deck script
        /// </summary>
        public void SaveDeck()
        {
            SaveDeck(ScriptFileName);
        }

        /// <summary>
        /// Save Deck script
        /// </summary>
        /// <param name="fileName">Deck script filename</param>
        public void SaveDeck(string fileName)
        {
            deckFolder = Path.GetDirectoryName(fileName);

            string text = "";
            string xmlFileName = Path.GetFileNameWithoutExtension(fileName) + ".xml";
            TemplateFileName = deckFolder + Path.DirectorySeparatorChar + xmlFileName;

            text += "template       " + xmlFileName + Environment.NewLine;
            try
            {
                if (Path.GetDirectoryName(DataFileName) == Path.GetDirectoryName(fileName))
                    text += "data           " + DataFileName + Environment.NewLine;
                else
                    text += "data           " + Path.GetFileName(DataFileName) + Environment.NewLine;
            }
            catch (Exception)
            {

            }

            text += "cardSize       " + cardSet.width.ToString() + ", " + cardSet.height.ToString() + ", " + cardSet.dpi + Environment.NewLine;
            text += "generate       " + genSet.genType.ToString() + ", " + genSet.genBackCards.ToString() + ", " + genSet.genTextInDoc.ToString() + ", " + genSet.genPaper.ToString() + ", " + genSet.genPapLandskape.ToString() + Environment.NewLine;



            Files.SaveFile(fileName, text);
            SaveXMLTemplate(TemplateFileName);

            CardFore.WorkPath = deckFolder;
            CardBack.WorkPath = deckFolder;

            IsChanged = false;
        }

        // ----- CARD -----
        /// <summary>
        /// Set new card size
        /// </summary>
        /// <param name="width">Card width [mm]</param>
        /// <param name="height">Card height [mm]</param>
        /// <param name="dpi">Card DPI</param>
        public void SetCardSize(int width, int height, int dpi)
        {
            cardSet.width = width;
            cardSet.height = height;
            cardSet.dpi = dpi;
            CardFore.SetSize(cardSet.width, cardSet.height, cardSet.dpi);
            CardBack.SetSize(cardSet.width, cardSet.height, cardSet.dpi);
        }


        /// <summary>
        /// Generate Deck
        /// </summary>
        public void Generate()
        {
            Directory.CreateDirectory(OutputFolder);
            string dataFile = Files.LoadFile(DataFileName);
            string[,] csvData = Files.LoadCSV(dataFile);
            string WordFile = OutputFolder + Path.DirectorySeparatorChar + Path.GetFileNameWithoutExtension(ScriptFileName) + ".docx";

            // Word
            WordEdit doc = new WordEdit();
            if (genSet.genType == GenType.docx)
            {
                if (!doc.Create(WordFile, doc.GetPageSize(genSet.genPaper, genSet.genPapLandskape)))
                {
                    Dialogs.ShowInfo("Cannot creating docx file!", "Error");
                    return;
                }
                //doc.SetMargins(10, 10, 10, 10);
            }

            TImgResult imageID;
            TImgResult imageBackID;


            string LastBackID = "";
            string lastTypeVal = null;
            TImgCardResult cardImgID;
            List<TImgCardResult> imgIDList = new List<TImgCardResult>();

            List<List<TextLocation>> listTxtLoxList = new List<List<TextLocation>>();

            int i = 0;
            for (i = 1; i < csvData.GetLength(1); i++)
            {
                List<TIDVal> list = new List<TIDVal>();
                for (int j = 0; j < csvData.GetLength(0); j++)
                {
                    TIDVal item = new TIDVal();
                    item.ID = csvData[j, 0].Trim();
                    item.value = csvData[j, i];
                    list.Add(item);
                }

                Image img;
                List<TextLocation> txtLocList = null;
                if (genSet.genTextInDoc)
                    txtLocList = this.CardFore.GetTextLoc(list);

                if (genSet.genType == GenType.png)
                {
                    img = this.CardFore.GetImage(list);
                    img.Save(OutputFolder + Path.DirectorySeparatorChar + "card" + i.ToString("D3") + ".png", ImageFormat.Png);
                    img.Dispose();
                }
                else if (genSet.genType == GenType.docx)
                {

                    img = this.CardFore.GetImage(list, !genSet.genTextInDoc);

                    imageID = doc.AddImage(Conv.ToStream(img, ImageFormat.Png), DocumentFormat.OpenXml.Packaging.ImagePartType.Png);
                    if (imageID.ID != "")
                    {
                        cardImgID = new TImgCardResult();
                        cardImgID.ID = imageID.ID;
                        cardImgID.backID = "";
                        cardImgID.width = imageID.width;
                        cardImgID.height = imageID.height;

                        if (genSet.genBackCards)
                        {
                            img.Dispose();
                            string typeVal = findTypeValue(list);
                            if (lastTypeVal == null)
                            {
                                img = this.CardBack.GetImage(list);
                                imageBackID = doc.AddImage(Conv.ToStream(img, ImageFormat.Png), DocumentFormat.OpenXml.Packaging.ImagePartType.Png);
                                LastBackID = imageBackID.ID;
                                lastTypeVal = typeVal;
                            }
                            else
                            {
                                if (typeVal != lastTypeVal)
                                {
                                    img = this.CardBack.GetImage(list);
                                    imageBackID = doc.AddImage(Conv.ToStream(img, ImageFormat.Png), DocumentFormat.OpenXml.Packaging.ImagePartType.Png);
                                    LastBackID = imageBackID.ID;
                                    lastTypeVal = typeVal;
                                }
                                else
                                {
                                    imageBackID.ID = LastBackID;
                                }
                            }

                            img.Dispose();
                            cardImgID.backID = imageBackID.ID;
                        }
                        int cardCount = GetCardCount(list);
                        for (int j = 0; j < cardCount; j++)
                        {
                            imgIDList.Add(cardImgID);
                            if (genSet.genTextInDoc)
                                listTxtLoxList.Add(txtLocList);
                        }

                    }
                    img.Dispose();
                }

            }

            if (genSet.genType == GenType.docx)
                i = imgIDList.Count;
            else
                i--;



            /*long size = 0;
            object o = new object();
            using (Stream s = new MemoryStream())
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(s, doc);
                size = s.Length;
            }*/

            if (genSet.genType == GenType.docx)
            {
                TPageSettings PaperSize = doc.GetPageSize(genSet.genPaper, genSet.genPapLandskape);
               
                int rowsPerPage, colsPerPage;
                int Margins = 10;

                colsPerPage = (PaperSize.Width - 2 * Margins) / cardSet.width;       // table Cols
                rowsPerPage = (PaperSize.Height - 2 * Margins) / cardSet.height;     // table Rows


                //doc.Paragraphs.Add("ahoj");
                int Rows = i / colsPerPage;
                if (i % colsPerPage > 0) Rows++;
                int tableCount = Rows / rowsPerPage;
                if (Rows % rowsPerPage > 0) tableCount++;
                int tableIndex = 0;
                int count = 0;
                for (int t = 0; t < tableCount; t++)
                {
                    doc.Paragraphs.AddTable(colsPerPage, rowsPerPage, cardSet.height, cardSet.width);
                    if (genSet.genBackCards)
                    {
                        //doc.Paragraphs.Add("");
                        doc.Paragraphs.AddTable(colsPerPage, rowsPerPage, cardSet.height, cardSet.width);
                    }


                    for (int j = 0; j < rowsPerPage; j++)
                    {
                        for (int k = 0; k < colsPerPage; k++)
                        {
                            if (count < imgIDList.Count)
                            {
                                TImgResult imgID = new TImgResult();
                                imgID.ID = imgIDList[count].ID;
                                imgID.width = imgIDList[count].width;
                                imgID.height = imgIDList[count].height;
                                doc.Paragraphs.FillTableCell(tableIndex, k, j, imgID);

                                if (genSet.genTextInDoc)
                                {
                                    for (int p = 0; p < listTxtLoxList[count].Count; p++)
                                    {
                                        TextBoxProps tbp = new TextBoxProps();
                                        tbp.text = listTxtLoxList[count][p].text;
                                        tbp.x = listTxtLoxList[count][p].x;
                                        tbp.y = listTxtLoxList[count][p].y;
                                        tbp.font = listTxtLoxList[count][p].font;
                                        tbp.alignment = (int)listTxtLoxList[count][p].alignment;
                                        tbp.width = listTxtLoxList[count][p].width;
                                        tbp.height = listTxtLoxList[count][p].height;
                                        tbp.color = listTxtLoxList[count][p].color;
                                        doc.Paragraphs.FillTableCell(tableIndex, k, j, tbp);
                                    }
                                }

                                if (genSet.genBackCards)
                                {
                                    imgID.ID = imgIDList[count].backID;
                                    doc.Paragraphs.FillTableCell(tableIndex + 1, colsPerPage - 1 - k, j, imgID);
                                }
                                count++;
                            }
                        }
                    }
                    tableIndex++;
                    if (genSet.genBackCards) tableIndex++;
                }


                doc.Save();
                doc.Close();
            }

            Dialogs.ShowInfo("Generate " + (i).ToString() + " cards.", "Generate done");
        }

        #endregion

        #region Private

        /// <summary>
        /// Create new deck
        /// </summary>
        /// <param name="width"></param>
        /// <param name="heigth"></param>
        /// <param name="dpi"></param>
        void createNewDeck(int width = 59, int heigth = 88, int dpi = 300)
        {
            cardSet = new TCardSet();       // card settings
            cardSet.width = width;
            cardSet.height = heigth;
            cardSet.dpi = dpi;
            genSet.genBackCards = true;     // generate settings
            genSet.genTextInDoc = false;
            genSet.genType = GenType.docx;

            if (ScriptFileName != "")
            {
                TemplateFileName = deckFolder + Path.DirectorySeparatorChar + Path.GetFileNameWithoutExtension(ScriptFileName) + ".xml";
                DataFileName = deckFolder + Path.DirectorySeparatorChar + Path.GetFileNameWithoutExtension(ScriptFileName) + ".csv";
                OutputFolder = deckFolder + Path.DirectorySeparatorChar + "Output";
            }
            else
            {
                OutputFolder = "Output";
            }
            
            CardFore = new CardSideTemplate(cardSet.width, cardSet.height, cardSet.dpi);
            CardBack = new CardSideTemplate(cardSet.width, cardSet.height, cardSet.dpi);
            CardFore.WorkPath = deckFolder;
            CardBack.WorkPath = deckFolder;

            IsChanged = true;                   // deck value was changed
        }

        // ----- LOAD / SAVE -----

        /// <summary>
        /// Load XML deck template
        /// </summary>
        /// <param name="fileName">XML filename</param>
        void LoadXMLTemplate(string fileName)
        {
            string att;

            TCardLabel label = null;
            TCardText text = null;
            TCardImg img = null;
            TCardShape shape = null;

            ESection section = ESection.none;

            try
            {

                using (XmlReader reader = XmlReader.Create(fileName))
                {
                    while (reader.Read())
                    {
                        if (section == ESection.none)
                        {
                            //reader.ReadStartElement();
                            switch (reader.NodeType)
                            {
                                case XmlNodeType.Element:
                                    if (reader.Name == "Deck") section = ESection.deck;
                                    break;
                                case XmlNodeType.Text:
                                    break;
                                case XmlNodeType.EndElement:
                                    section = ESection.none;
                                    break;
                            }
                        }

                        if (section == ESection.deck)
                        {
                            // reader.ReadStartElement();
                            switch (reader.NodeType)
                            {
                                case XmlNodeType.Element:
                                    if (reader.Name == "Settings") section = ESection.settings;
                                    if (reader.Name == "ForeCard") section = ESection.foreCard;
                                    if (reader.Name == "BackCard") section = ESection.backCard;
                                    break;
                                case XmlNodeType.Text:
                                    break;
                                case XmlNodeType.EndElement:
                                    if (reader.Name == "Deck") section = ESection.none;
                                    break;
                            }
                        }

                        if (section == ESection.settings)
                        {
                            //reader.ReadStartElement();
                            switch (reader.NodeType)
                            {
                                case XmlNodeType.Element:
                                    if (reader.IsEmptyElement) section = ESection.deck;
                                    break;
                                case XmlNodeType.Text:
                                    break;
                                case XmlNodeType.EndElement:
                                    if (reader.Name == "Settings")
                                        section = ESection.deck;
                                    break;
                            }
                        }

                        if (section == ESection.foreCard || section == ESection.backCard)
                        {
                            //reader.ReadStartElement();
                            switch (reader.NodeType)
                            {
                                case XmlNodeType.Element:

                                    if (reader.Name == "ForeCard" && reader.IsEmptyElement) section = ESection.deck;
                                    if (reader.Name == "BackCard" && reader.IsEmptyElement) section = ESection.deck;

                                    // ----- LABEL ----------------------------------------------------
                                    if (reader.Name == "label")
                                    {
                                        label = new TCardLabel();
                                        label.show = true;

                                        att = reader.GetAttribute("ID");
                                        if (att != null) label.ID = att;
                                        else label.ID = "label";

                                        att = reader.GetAttribute("type");
                                        if (att != null) label.type = att;
                                        else label.type = "";

                                        label.location = new TPoint(0, 0);
                                        att = reader.GetAttribute("location");
                                        if (att != null)
                                        {
                                            string[] split = att.Split(new string[] { ";" }, StringSplitOptions.None);
                                            if (split.Length == 2)
                                            {
                                                TPointValue pointXVal = new TPointValue(0, CardUnits.percentage);
                                                if (split[0].Contains("px"))
                                                {
                                                    pointXVal = new TPointValue(Conv.ToIntDef(split[0].Replace("px", ""), 0), CardUnits.px);
                                                }
                                                else if (split[0].Contains("mm"))
                                                {
                                                    pointXVal = new TPointValue(Conv.ToIntDef(split[0].Replace("mm", ""), 0), CardUnits.mm);
                                                }
                                                else
                                                {
                                                    pointXVal = new TPointValue(Conv.ToIntDef(split[0], 0), CardUnits.percentage);
                                                }
                                                TPointValue pointYVal = new TPointValue(0, CardUnits.percentage);
                                                if (split[1].Contains("px"))
                                                {
                                                    pointYVal = new TPointValue(Conv.ToIntDef(split[1].Replace("px", ""), 0), CardUnits.px);
                                                }
                                                else if (split[1].Contains("mm"))
                                                {
                                                    pointYVal = new TPointValue(Conv.ToIntDef(split[1].Replace("mm", ""), 0), CardUnits.mm);
                                                }
                                                else
                                                {
                                                    pointYVal = new TPointValue(Conv.ToIntDef(split[1], 0), CardUnits.percentage);
                                                }
                                                label.location = new TPoint(pointXVal, pointYVal);
                                            }
                                        }

                                        att = reader.GetAttribute("angle");
                                        if (att != null) label.angle = Conv.ToIntDef(att, 0);
                                        else label.angle = 0;

                                        att = reader.GetAttribute("color");
                                        if (att != null) label.color = Conv.GetColor(att);
                                        else label.color = Color.Black;

                                        att = reader.GetAttribute("backColor");
                                        if (att != null) label.backColor = Conv.GetColor(att);
                                        else label.backColor = Color.Black;

                                        //att = reader.GetAttribute("fontSize");
                                        //if (att != null) label.size = Conv.ToIntDef(att, 10);
                                        //else label.size = 10;

                                        var cvt = new FontConverter();
                                        att = reader.GetAttribute("font");
                                        if (att != null)
                                        {
                                            try
                                            {
                                                label.font = cvt.ConvertFromInvariantString(att) as Font;
                                            }
                                            catch (Exception)
                                            {
                                                label.font = new Font("Arial", 10);
                                            }
                                        }
                                        else label.font = new Font("Arial", 10);

                                        att = reader.GetAttribute("capitalize");
                                        if (att != null) label.capitalize = Conv.ToBoolDef(att, false);
                                        else label.capitalize = false;

                                        att = reader.GetAttribute("align");
                                        try
                                        {
                                            label.alignment = (Align)Enum.Parse(typeof(Align), att, true);
                                        }
                                        catch (Exception) { label.alignment = Align.left; }

                                        if (reader.IsEmptyElement)
                                        {
                                            if (section == ESection.backCard)
                                                CardBack.AddLabel(label);
                                            else
                                                CardFore.AddLabel(label);
                                            label = null;
                                        }
                                    }
                                    // ----- TEXT ----------------------------------------------------
                                    else if (reader.Name == "text")
                                    {
                                        text = new TCardText();
                                        text.show = true;

                                        att = reader.GetAttribute("ID");
                                        if (att != null) text.ID = att;
                                        else text.ID = "text";

                                        att = reader.GetAttribute("type");
                                        if (att != null) text.type = att;
                                        else text.type = "";

                                        text.location = new TPoint(0, 0);
                                        att = reader.GetAttribute("location");
                                        if (att != null)
                                        {
                                            string[] split = att.Split(new string[] { ";" }, StringSplitOptions.None);
                                            if (split.Length == 2)
                                            {
                                                TPointValue pointXVal = new TPointValue(0, CardUnits.percentage);
                                                if (split[0].Contains("px"))
                                                {
                                                    pointXVal = new TPointValue(Conv.ToIntDef(split[0].Replace("px", ""), 0), CardUnits.px);
                                                }
                                                else if (split[1].Contains("mm"))
                                                {
                                                    pointXVal = new TPointValue(Conv.ToIntDef(split[0].Replace("mm", ""), 0), CardUnits.mm);
                                                }
                                                else
                                                {
                                                    pointXVal = new TPointValue(Conv.ToIntDef(split[0], 0), CardUnits.percentage);
                                                }
                                                TPointValue pointYVal = new TPointValue(0, CardUnits.percentage);
                                                if (split[0].Contains("px"))
                                                {
                                                    pointYVal = new TPointValue(Conv.ToIntDef(split[1].Replace("px", ""), 0), CardUnits.px);
                                                }
                                                else if (split[1].Contains("mm"))
                                                {
                                                    pointYVal = new TPointValue(Conv.ToIntDef(split[1].Replace("mm", ""), 0), CardUnits.mm);
                                                }
                                                else
                                                {
                                                    pointYVal = new TPointValue(Conv.ToIntDef(split[1], 0), CardUnits.percentage);
                                                }
                                                text.location = new TPoint(pointXVal, pointYVal);

                                            }
                                        }

                                        text.size = new TPoint(20, 20);
                                        att = reader.GetAttribute("size");
                                        if (att != null)
                                        {
                                            string[] split = att.Split(new string[] { ";" }, StringSplitOptions.None);
                                            if (split.Length == 2)
                                            {
                                                TPointValue pointXVal = new TPointValue(0, CardUnits.percentage);
                                                if (split[0].Contains("px"))
                                                {
                                                    pointXVal = new TPointValue(Conv.ToIntDef(split[0].Replace("px", ""), 20), CardUnits.px);
                                                }
                                                else if (split[1].Contains("mm"))
                                                {
                                                    pointXVal = new TPointValue(Conv.ToIntDef(split[0].Replace("mm", ""), 20), CardUnits.mm);
                                                }
                                                else
                                                {
                                                    pointXVal = new TPointValue(Conv.ToIntDef(split[0], 20), CardUnits.percentage);
                                                }
                                                TPointValue pointYVal = new TPointValue(0, CardUnits.percentage);
                                                if (split[0].Contains("px"))
                                                {
                                                    pointYVal = new TPointValue(Conv.ToIntDef(split[1].Replace("px", ""), 20), CardUnits.px);
                                                }
                                                else if (split[1].Contains("mm"))
                                                {
                                                    pointYVal = new TPointValue(Conv.ToIntDef(split[1].Replace("mm", ""), 20), CardUnits.mm);
                                                }
                                                else
                                                {
                                                    pointYVal = new TPointValue(Conv.ToIntDef(split[1], 20), CardUnits.percentage);
                                                }
                                                text.size = new TPoint(pointXVal, pointYVal);
                                            }
                                        }

                                        att = reader.GetAttribute("angle");
                                        if (att != null) text.angle = Conv.ToIntDef(att, 0);
                                        else text.angle = 0;

                                        att = reader.GetAttribute("color");
                                        if (att != null) text.color = Conv.GetColor(att);
                                        else text.color = Color.Black;

                                        att = reader.GetAttribute("backColor");
                                        if (att != null) text.backColor = Conv.GetColor(att);
                                        else text.backColor = Color.Black;


                                        var cvt = new FontConverter();
                                        att = reader.GetAttribute("font");
                                        if (att != null)
                                        {
                                            try
                                            {
                                                text.font = cvt.ConvertFromInvariantString(att) as Font;
                                            }
                                            catch (Exception)
                                            {
                                                text.font = new Font("Arial", 10);
                                            }
                                        }
                                        else text.font = new Font("Arial", 10);

                                        att = reader.GetAttribute("align");
                                        try
                                        {
                                            text.alignment = (Align)Enum.Parse(typeof(Align), att, true);
                                        }
                                        catch (Exception) { text.alignment = Align.left; }

                                        att = reader.GetAttribute("alignHeight");
                                        try
                                        {
                                            text.alignHeight = (Align)Enum.Parse(typeof(Align), att, true);
                                        }
                                        catch (Exception) { text.alignHeight = Align.left; }

                                        if (reader.IsEmptyElement)
                                        {
                                            if (section == ESection.backCard)
                                                CardBack.AddText(text);
                                            else
                                                CardFore.AddText(text);
                                            text = null;
                                        }
                                    }
                                    else if (reader.Name == "image")
                                    {
                                        img = new TCardImg();
                                        img.show = true;
                                        img.path = "";

                                        att = reader.GetAttribute("ID");
                                        if (att != null) img.ID = att;
                                        else img.ID = "image";

                                        att = reader.GetAttribute("type");
                                        if (att != null) img.type = att;
                                        else img.type = "";

                                        img.location = new TPoint(0, 0);
                                        att = reader.GetAttribute("location");
                                        if (att != null)
                                        {
                                            string[] split = att.Split(new string[] { ";" }, StringSplitOptions.None);
                                            if (split.Length == 2)
                                            {
                                                TPointValue pointXVal = new TPointValue(0, CardUnits.percentage);
                                                if (split[0].Contains("px"))
                                                {
                                                    pointXVal = new TPointValue(Conv.ToIntDef(split[0].Replace("px", ""), 0), CardUnits.px);
                                                }
                                                else if (split[0].Contains("mm"))
                                                {
                                                    pointXVal = new TPointValue(Conv.ToIntDef(split[0].Replace("mm", ""), 0), CardUnits.mm);
                                                }
                                                else
                                                {
                                                    pointXVal = new TPointValue(Conv.ToIntDef(split[0], 0), CardUnits.percentage);
                                                }
                                                TPointValue pointYVal = new TPointValue(0, CardUnits.percentage);
                                                if (split[1].Contains("px"))
                                                {
                                                    pointYVal = new TPointValue(Conv.ToIntDef(split[1].Replace("px", ""), 0), CardUnits.px);
                                                }
                                                else if (split[1].Contains("mm"))
                                                {
                                                    pointYVal = new TPointValue(Conv.ToIntDef(split[1].Replace("mm", ""), 0), CardUnits.mm);
                                                }
                                                else
                                                {
                                                    pointYVal = new TPointValue(Conv.ToIntDef(split[1], 0), CardUnits.percentage);
                                                }
                                                img.location = new TPoint(pointXVal, pointYVal);
                                            }
                                        }

                                        att = reader.GetAttribute("angle");
                                        if (att != null) img.angle = Conv.ToIntDef(att, 0);
                                        else img.angle = 0;

                                        img.size = new TImgPoint(0, 0);
                                        att = reader.GetAttribute("size");
                                        if (att != null)
                                        {
                                            string[] split = att.Split(new string[] { ";" }, StringSplitOptions.None);
                                            if (split.Length == 2)
                                            {
                                                TImgPointValue pointXVal = new TImgPointValue(0, CardImgUnits.percentage);
                                                if (split[0].Contains("px"))
                                                {
                                                    pointXVal = new TImgPointValue(Conv.ToIntDef(split[0].Replace("px", ""), 0), CardImgUnits.px);
                                                }
                                                else if (split[0].Contains("mm"))
                                                {
                                                    pointXVal = new TImgPointValue(Conv.ToIntDef(split[0].Replace("mm", ""), 0), CardImgUnits.mm);
                                                }
                                                else if (split[0].Contains("img"))
                                                {
                                                    pointXVal = new TImgPointValue(Conv.ToIntDef(split[0].Replace("img", ""), 0), CardImgUnits.imgPerc);
                                                }
                                                else
                                                {
                                                    pointXVal = new TImgPointValue(Conv.ToIntDef(split[0], 0), CardImgUnits.percentage);
                                                }
                                                TImgPointValue pointYVal = new TImgPointValue(0, CardImgUnits.percentage);
                                                if (split[1].Contains("px"))
                                                {
                                                    pointYVal = new TImgPointValue(Conv.ToIntDef(split[1].Replace("px", ""), 0), CardImgUnits.px);
                                                }
                                                else if (split[1].Contains("mm"))
                                                {
                                                    pointYVal = new TImgPointValue(Conv.ToIntDef(split[1].Replace("mm", ""), 0), CardImgUnits.mm);
                                                }
                                                else if (split[1].Contains("img"))
                                                {
                                                    pointYVal = new TImgPointValue(Conv.ToIntDef(split[1].Replace("img", ""), 0), CardImgUnits.imgPerc);
                                                }
                                                else
                                                {
                                                    pointYVal = new TImgPointValue(Conv.ToIntDef(split[1], 0), CardImgUnits.percentage);
                                                }
                                                img.size = new TImgPoint(pointXVal, pointYVal);
                                            }
                                        }

                                        att = reader.GetAttribute("align");
                                        try
                                        {
                                            img.alignment = (Align)Enum.Parse(typeof(Align), att, true);
                                        }
                                        catch (Exception) { img.alignment = Align.left; }

                                        att = reader.GetAttribute("alignHeight");
                                        try
                                        {
                                            img.alignHeight = (Align)Enum.Parse(typeof(Align), att, true);
                                        }
                                        catch (Exception) { img.alignHeight = Align.left; }

                                        if (reader.IsEmptyElement)
                                        {
                                            if (section == ESection.backCard)
                                                CardBack.AddImage(img);
                                            else
                                                CardFore.AddImage(img);

                                            img = null;
                                        }
                                    }
                                    else if (reader.Name == "shape")
                                    {
                                        shape = new TCardShape();
                                        shape.show = true;

                                        att = reader.GetAttribute("ID");
                                        if (att != null) shape.ID = att;
                                        else shape.ID = "shape";

                                        att = reader.GetAttribute("type");
                                        if (att != null) shape.type = att;
                                        else shape.type = "";

                                        att = reader.GetAttribute("object");
                                        try
                                        {
                                            shape.shape = (Shape)Enum.Parse(typeof(Shape), att, true);
                                        }
                                        catch (Exception) { shape.shape = Shape.rectangle; }

                                        att = reader.GetAttribute("color");
                                        if (att != null) shape.color = Conv.GetColor(att);
                                        else shape.color = Color.Black;

                                        att = reader.GetAttribute("backColor");
                                        if (att != null) shape.fillColor = Conv.GetColor(att);
                                        else shape.fillColor = Color.Transparent;


                                        att = reader.GetAttribute("penSize");
                                        shape.penSize = Conv.ToIntDef(att, 3);

                                        att = reader.GetAttribute("round");
                                        if (att != null)
                                        {
                                            if (att.Contains("px"))
                                            {
                                                shape.round = new TPointValue(Conv.ToIntDef(att.Replace("px", ""), 0), CardUnits.px);
                                            }
                                            else if (att.Contains("mm"))
                                            {
                                                shape.round = new TPointValue(Conv.ToIntDef(att.Replace("mm", ""), 0), CardUnits.mm);
                                            }
                                            else
                                            {
                                                shape.round = new TPointValue(Conv.ToIntDef(att, 0), CardUnits.percentage);
                                            }
                                        }
                                        else shape.round = new TPointValue(Conv.ToIntDef(att, 0), CardUnits.percentage);

                                        shape.location = new TPoint(0, 0);
                                        att = reader.GetAttribute("location");
                                        if (att != null)
                                        {
                                            string[] split = att.Split(new string[] { ";" }, StringSplitOptions.None);
                                            if (split.Length == 2)
                                            {
                                                TPointValue pointXVal = new TPointValue(0, CardUnits.percentage);
                                                if (split[0].Contains("px"))
                                                {
                                                    pointXVal = new TPointValue(Conv.ToIntDef(split[0].Replace("px", ""), 0), CardUnits.px);
                                                }
                                                else if (split[1].Contains("mm"))
                                                {
                                                    pointXVal = new TPointValue(Conv.ToIntDef(split[0].Replace("mm", ""), 0), CardUnits.mm);
                                                }
                                                else
                                                {
                                                    pointXVal = new TPointValue(Conv.ToIntDef(split[0], 0), CardUnits.percentage);
                                                }
                                                TPointValue pointYVal = new TPointValue(0, CardUnits.percentage);
                                                if (split[0].Contains("px"))
                                                {
                                                    pointYVal = new TPointValue(Conv.ToIntDef(split[1].Replace("px", ""), 0), CardUnits.px);
                                                }
                                                else if (split[1].Contains("mm"))
                                                {
                                                    pointYVal = new TPointValue(Conv.ToIntDef(split[1].Replace("mm", ""), 0), CardUnits.mm);
                                                }
                                                else
                                                {
                                                    pointYVal = new TPointValue(Conv.ToIntDef(split[1], 0), CardUnits.percentage);
                                                }
                                                shape.location = new TPoint(pointXVal, pointYVal);

                                            }
                                        }

                                        shape.size = new TPoint(20, 20);
                                        att = reader.GetAttribute("size");
                                        if (att != null)
                                        {
                                            string[] split = att.Split(new string[] { ";" }, StringSplitOptions.None);
                                            if (split.Length == 2)
                                            {
                                                TPointValue pointXVal = new TPointValue(0, CardUnits.percentage);
                                                if (split[0].Contains("px"))
                                                {
                                                    pointXVal = new TPointValue(Conv.ToIntDef(split[0].Replace("px", ""), 20), CardUnits.px);
                                                }
                                                else if (split[1].Contains("mm"))
                                                {
                                                    pointXVal = new TPointValue(Conv.ToIntDef(split[0].Replace("mm", ""), 20), CardUnits.mm);
                                                }
                                                else
                                                {
                                                    pointXVal = new TPointValue(Conv.ToIntDef(split[0], 20), CardUnits.percentage);
                                                }
                                                TPointValue pointYVal = new TPointValue(0, CardUnits.percentage);
                                                if (split[0].Contains("px"))
                                                {
                                                    pointYVal = new TPointValue(Conv.ToIntDef(split[1].Replace("px", ""), 20), CardUnits.px);
                                                }
                                                else if (split[1].Contains("mm"))
                                                {
                                                    pointYVal = new TPointValue(Conv.ToIntDef(split[1].Replace("mm", ""), 20), CardUnits.mm);
                                                }
                                                else
                                                {
                                                    pointYVal = new TPointValue(Conv.ToIntDef(split[1], 20), CardUnits.percentage);
                                                }
                                                shape.size = new TPoint(pointXVal, pointYVal);
                                            }
                                        }

                                        att = reader.GetAttribute("angle");
                                        if (att != null) shape.angle = Conv.ToIntDef(att, 0);
                                        else shape.angle = 0;

                                        att = reader.GetAttribute("startAngle");
                                        if (att != null) shape.startAngle = Conv.ToIntDef(att, 0);
                                        else shape.startAngle = 0;

                                        att = reader.GetAttribute("sweepAngle");
                                        if (att != null) shape.sweepAngle = Conv.ToIntDef(att, 0);
                                        else shape.sweepAngle = 0;


                                        att = reader.GetAttribute("align");
                                        try
                                        {
                                            shape.alignment = (Align)Enum.Parse(typeof(Align), att, true);
                                        }
                                        catch (Exception) { shape.alignment = Align.left; }

                                        att = reader.GetAttribute("alignHeight");
                                        try
                                        {
                                            shape.alignHeight = (Align)Enum.Parse(typeof(Align), att, true);
                                        }
                                        catch (Exception) { shape.alignHeight = Align.left; }

                                        if (section == ESection.backCard)
                                            CardBack.AddShape(shape);
                                        else
                                            CardFore.AddShape(shape);


                                    }
                                    break;
                                case XmlNodeType.Text:
                                    if (img != null) img.path = reader.Value;
                                    if (label != null) label.text = reader.Value;
                                    if (text != null) text.text = reader.Value;
                                    break;
                                case XmlNodeType.EndElement:
                                    if (img != null)
                                    {
                                        if (section == ESection.backCard)
                                            CardBack.AddImage(img);
                                        else
                                            CardFore.AddImage(img);
                                        img = null;
                                    }

                                    if (label != null)
                                    {
                                        if (section == ESection.backCard)
                                            CardBack.AddLabel(label);
                                        else
                                            CardFore.AddLabel(label);
                                        label = null;
                                    }

                                    if (text != null)
                                    {
                                        if (section == ESection.backCard)
                                            CardBack.AddText(text);
                                        else
                                            CardFore.AddText(text);
                                        text = null;
                                    }

                                    if (reader.Name == "ForeCard" || reader.Name == "BackCard")
                                        section = ESection.deck;
                                    break;
                            }
                        }
                    }

                }

            } catch(Exception) { }
        }

        /// <summary>
        /// Save XML deck template
        /// </summary>
        /// <param name="fileName">XML filaname</param>
        void SaveXMLTemplate(string fileName)
        {
            using (XmlWriter writer = XmlWriter.Create(fileName))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("Deck");

                writer.WriteStartElement("Settings");

                writer.WriteEndElement();

                writer.WriteStartElement("ForeCard");

                WriteCardTemp(writer, CardFore);

                writer.WriteEndElement();

                writer.WriteStartElement("BackCard");

                WriteCardTemp(writer, CardBack);

                writer.WriteEndElement();

                writer.WriteEndElement();
                writer.WriteEndDocument();
            }
        }

        /// <summary>
        /// Write template of one card side
        /// </summary>
        /// <param name="writer">Used XML writer</param>
        /// <param name="card">Card side to write</param>
        private void WriteCardTemp(XmlWriter writer, CardSideTemplate card)
        {
            string unit, unit2;

            for (int i = 0; i < card.itemList.Count; i++)
            {
                switch (card.itemList[i].type)
                {
                    case CardObjectType.label:
                        TCardLabel label = card.labelList[card.itemList[i].index];
                        writer.WriteStartElement("label");   // <-- These are new
                        writer.WriteAttributeString("ID", label.ID);
                        writer.WriteAttributeString("type", label.type);
                        if (label.location.x.Unit == CardUnits.px) unit = "px";
                        else if (label.location.x.Unit == CardUnits.mm) unit = "mm";
                        else unit = "";
                        if (label.location.y.Unit == CardUnits.px) unit2 = "px";
                        else if (label.location.y.Unit == CardUnits.mm) unit2 = "mm";
                        else unit2 = "";
                        writer.WriteAttributeString("location", label.location.x.Value.ToString() + unit + "; " + label.location.y.Value.ToString() + unit2);
                        writer.WriteAttributeString("angle", label.angle.ToString());

                        writer.WriteAttributeString("color", label.color.Name.ToString());
                        writer.WriteAttributeString("backColor", label.backColor.Name.ToString());

                        var cvt = new FontConverter();
                        writer.WriteAttributeString("font", cvt.ConvertToInvariantString(label.font));
                        writer.WriteAttributeString("capitalize", label.capitalize.ToString());
                        //writer.WriteAttributeString("fontSize", label.size.ToString());
                        writer.WriteAttributeString("align", label.alignment.ToString());

                        writer.WriteString(label.text);
                        writer.WriteEndElement();
                        break;

                    case CardObjectType.text:
                        TCardText text = card.textList[card.itemList[i].index];
                        writer.WriteStartElement("text");   // <-- These are new
                        writer.WriteAttributeString("ID", text.ID);
                        writer.WriteAttributeString("type", text.type);
                        if (text.location.x.Unit == CardUnits.px) unit = "px";
                        else if (text.location.x.Unit == CardUnits.mm) unit = "mm";
                        else unit = "";
                        if (text.location.y.Unit == CardUnits.px) unit2 = "px";
                        else if (text.location.y.Unit == CardUnits.mm) unit2 = "mm";
                        else unit2 = "";
                        writer.WriteAttributeString("location", text.location.x.Value.ToString() + unit + "; " + text.location.y.Value.ToString() + unit2);

                        if (text.size.x.Unit == CardUnits.px) unit = "px";
                        else if (text.size.x.Unit == CardUnits.mm) unit = "mm";
                        else unit = "";
                        if (text.size.y.Unit == CardUnits.px) unit2 = "px";
                        else if (text.size.y.Unit == CardUnits.mm) unit2 = "mm";
                        else unit2 = "";
                        writer.WriteAttributeString("size", text.size.x.Value.ToString() + unit + "; " + text.size.y.Value.ToString() + unit2);

                        writer.WriteAttributeString("angle", text.angle.ToString());

                        writer.WriteAttributeString("color", text.color.Name.ToString());
                        writer.WriteAttributeString("backColor", text.backColor.Name.ToString());

                        var cvtt = new FontConverter();
                        writer.WriteAttributeString("font", cvtt.ConvertToInvariantString(text.font));
                        writer.WriteAttributeString("align", text.alignment.ToString());
                        writer.WriteAttributeString("alignHeight", text.alignHeight.ToString());

                        writer.WriteString(text.text);
                        writer.WriteEndElement();
                        break;
                    case CardObjectType.image:
                        TCardImg img = card.imgList[card.itemList[i].index];
                        writer.WriteStartElement("image");   // <-- These are new
                        writer.WriteAttributeString("ID", img.ID);
                        writer.WriteAttributeString("type", img.type);
                        if (img.location.x.Unit == CardUnits.px) unit = "px";
                        else if (img.location.x.Unit == CardUnits.mm) unit = "mm";
                        else unit = "";
                        if (img.location.y.Unit == CardUnits.px) unit2 = "px";
                        else if (img.location.y.Unit == CardUnits.mm) unit2 = "mm";
                        else unit2 = "";
                        writer.WriteAttributeString("location", img.location.x.Value.ToString() + unit + "; " + img.location.y.Value.ToString() + unit2);
                        if (img.size.x.Unit == CardImgUnits.px) unit = "px";
                        else if (img.size.x.Unit == CardImgUnits.mm) unit = "mm";
                        else if (img.size.x.Unit == CardImgUnits.imgPerc) unit = "img";
                        else unit = "";
                        if (img.size.y.Unit == CardImgUnits.px) unit2 = "px";
                        else if (img.size.y.Unit == CardImgUnits.mm) unit2 = "mm";
                        else if (img.size.x.Unit == CardImgUnits.imgPerc) unit2 = "img";
                        else unit2 = "";
                        writer.WriteAttributeString("size", img.size.x.Value.ToString() + unit + "; " + img.size.y.Value.ToString() + unit2);

                        writer.WriteAttributeString("angle", img.angle.ToString());
                        writer.WriteAttributeString("align", img.alignment.ToString());
                        writer.WriteAttributeString("alignHeight", img.alignHeight.ToString());

                        writer.WriteString(img.path);
                        writer.WriteEndElement();
                        break;
                    case CardObjectType.shape:
                        TCardShape shape = card.shapeList[card.itemList[i].index];
                        writer.WriteStartElement("shape");   // <-- These are new
                        writer.WriteAttributeString("ID", shape.ID);
                        writer.WriteAttributeString("type", shape.type);
                        writer.WriteAttributeString("object", shape.shape.ToString());
                        writer.WriteAttributeString("color", shape.color.Name.ToString());
                        writer.WriteAttributeString("backColor", shape.fillColor.Name.ToString());
                        writer.WriteAttributeString("penSize", shape.penSize.ToString());



                        if (shape.round.Unit == CardUnits.px) unit = "px";
                        else if (shape.round.Unit == CardUnits.mm) unit = "mm";
                        else unit = "";
                        writer.WriteAttributeString("round", shape.round.Value.ToString() + unit);


                        if (shape.location.x.Unit == CardUnits.px) unit = "px";
                        else if (shape.location.x.Unit == CardUnits.mm) unit = "mm";
                        else unit = "";
                        if (shape.location.y.Unit == CardUnits.px) unit2 = "px";
                        else if (shape.location.y.Unit == CardUnits.mm) unit2 = "mm";
                        else unit2 = "";
                        writer.WriteAttributeString("location", shape.location.x.Value.ToString() + unit + "; " + shape.location.y.Value.ToString() + unit2);

                        if (shape.size.x.Unit == CardUnits.px) unit = "px";
                        else if (shape.size.x.Unit == CardUnits.mm) unit = "mm";
                        else unit = "";
                        if (shape.size.y.Unit == CardUnits.px) unit2 = "px";
                        else if (shape.size.y.Unit == CardUnits.mm) unit2 = "mm";
                        else unit2 = "";
                        writer.WriteAttributeString("size", shape.size.x.Value.ToString() + unit + "; " + shape.size.y.Value.ToString() + unit2);

                        writer.WriteAttributeString("angle", shape.angle.ToString());
                        writer.WriteAttributeString("startAngle", shape.startAngle.ToString());
                        writer.WriteAttributeString("sweepAngle", shape.sweepAngle.ToString());


                        writer.WriteAttributeString("align", shape.alignment.ToString());
                        writer.WriteAttributeString("alignHeight", shape.alignHeight.ToString());

                        //writer.WriteString("");
                        writer.WriteEndElement();
                        break;
                }

            }
        }

        // ----- GENERATE -----

        /// <summary>
        /// Get card count from data
        /// </summary>
        /// <param name="list">Used data list</param>
        /// <returns>Card count for generating</returns>
        private int GetCardCount(List<TIDVal> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].ID == "#count")
                {
                    return Conv.ToIntDef(list[i].value, 1);
                }
            }
            return 1;
        }

        /// <summary>
        /// Returns card type from data
        /// </summary>
        /// <param name="list">Using data list</param>
        /// <returns>Returns card type from data</returns>
        private string findTypeValue(List<TIDVal> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].ID == "type")
                {
                    return list[i].value;
                }

            }
            return "";
        }

        #endregion




    }

    public class Card
    {
        CardImg cardImg;

        public Card()
        {
            cardImg = new CardImg(60, 90, 150);
        }

        public Card(int width, int height, int DPI = 150)
        {
            cardImg = new CardImg(width, height, DPI);
        }
    }

    public class CardImg
    {
        Image cardImg;

        public int Width { get; }
        public int Height { get; }

        public CardImg(int width, int height, int DPI)
        {
            Width = width;
            Height = height;
            int wPix = (int)(0.03937 * width * DPI);
            int hPix = (int)(0.03937 * height * DPI);

            Bitmap b = new Bitmap(wPix, hPix);
            b.SetResolution(DPI, DPI);
            cardImg = b;
            SetBackground();
        }

        public Image Image()
        {
            return cardImg;
        }

        public void SetBackground(Color? backColor = null)
        {
            Graphics drawing = Graphics.FromImage(cardImg);
            drawing.Clear(backColor ?? Color.White);
            drawing.Save();
            drawing.Dispose();
        }

        public void DrawLabel(string text, TPoint location, int angle = 0, Font font = null, Color? textColor = null, Color? backColor = null, Align alignment = Align.left)
        {
            Graphics drawing = Graphics.FromImage(cardImg);
            drawing.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            StringFormat format = new StringFormat();

            float x = location.x.Value, y = location.y.Value;

            //paint the background
            //if (backColor != null)
            //    drawing.Clear(backColor ?? Color.White);

            //create a brush for the text
            Brush textBrush = new SolidBrush(textColor ?? Color.Black);
            if (font == null) font = new Font("Arial", 10);
            if (alignment == Align.center)
            {
                format = new StringFormat()    // center
                {
                    Alignment = StringAlignment.Center
                    //,LineAlignment = StringAlignment.Center
                };
            }
            else if (alignment == Align.right)
            {
                format = new StringFormat()    // center
                {
                    Alignment = StringAlignment.Far
                    //,LineAlignment = StringAlignment.Center
                };

                //format = new StringFormat(StringFormatFlags.DirectionRightToLeft);
            }

            if (location.x.Unit == CardUnits.percentage)
            {
                x = (cardImg.Width * x) / 100;
            }
            if (location.y.Unit == CardUnits.percentage)
            {
                y = (cardImg.Height * y) / 100;
            }
            drawing.TranslateTransform(x, y);
            drawing.RotateTransform(angle);
            drawing.DrawString(text, font, textBrush, 0, 0, format);

            drawing.Save();

            textBrush.Dispose();
            drawing.Dispose();



        }

        public void DrawText(string text, TPoint location, TPoint size, int angle = 0, Font font = null, Color? textColor = null, Color? backColor = null, Align alignment = Align.left, Align alignHeight = Align.left)
        {
            Graphics drawing = Graphics.FromImage(cardImg);
            drawing.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            StringFormat format = new StringFormat();

            float x = location.x.Value, y = location.y.Value;
            float w = size.x.Value, h = size.y.Value;

            //paint the background
            //if (backColor != null)
            //    drawing.Clear(backColor ?? Color.White);

            //create a brush for the text
            Brush textBrush = new SolidBrush(textColor ?? Color.Black);
            if (font == null) font = new Font("Arial", 10);

            StringAlignment align = StringAlignment.Near;
            StringAlignment alignH = StringAlignment.Near;

            if (alignment == Align.center) align = StringAlignment.Center;
            if (alignment == Align.right) align = StringAlignment.Far;
            if (alignHeight == Align.center) alignH = StringAlignment.Center;
            if (alignHeight == Align.right) alignH = StringAlignment.Far;

            format = new StringFormat()    // center
            {
                Alignment = align
                ,LineAlignment = alignH
            };

            if (location.x.Unit == CardUnits.percentage)
            {
                x = (cardImg.Width * x) / 100;
            }
            if (location.y.Unit == CardUnits.percentage)
            {
                y = (cardImg.Height * y) / 100;
            }
            if (size.x.Unit == CardUnits.percentage)
            {
                w = (cardImg.Width * w) / 100;
            }
            if (size.y.Unit == CardUnits.percentage)
            {
                h = (cardImg.Height * h) / 100;
            }


            drawing.TranslateTransform(x, y);
            drawing.RotateTransform(angle);
            RectangleF rect = new RectangleF(0, 0, w, h);

            drawing.DrawString(text, font, textBrush, rect, format);

            Rectangle rect2 = new Rectangle(0, 0, (int)w, (int)h);

            /*RichTextBox richTextBox = new RichTextBox();
            richTextBox.Text = text;
            string rtfFormattedString = richTextBox.Rtf;
            Graphics_DrawRtfText.DrawRtfText(drawing, rtfFormattedString, rect2);*/



                drawing.Save();

            textBrush.Dispose();
            drawing.Dispose();



        }

        public void DrawImage(string fileName, TPoint location, TImgPoint size, int angle = 0, Align alingment = Align.left, Align alignmentHeigth = Align.left)
        {
            float x = location.x.Value, y = location.y.Value, width = size.x.Value, height = size.y.Value;
            float setWidth = width, setHeight = height;

            Image upImg;
            SvgDocument document = null;
            try
            {
                if (Path.GetExtension(fileName) == ".svg")
                {
                    document = SvgDocument.Open(fileName);
                    upImg = document.Draw();
                }
                else
                {
                    upImg = System.Drawing.Image.FromFile(fileName);
                }

            }
            catch (Exception)
            {
                int widthI = 40, heightI = 40;
                upImg = new Bitmap(40, 40);
                Graphics gI = Graphics.FromImage(upImg);
                gI.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                Brush brush = new SolidBrush(Color.WhiteSmoke);
                Pen pen = new Pen(Color.Black, 1);
                gI.FillRectangle(brush, 0, 0, widthI - 1, heightI - 1);
                gI.DrawRectangle(pen, 0, 0, widthI - 1, heightI - 1);
                pen = new Pen(Color.OrangeRed, 2);
                gI.DrawLine(pen, 3, 3, widthI - 4, heightI - 4);
                gI.DrawLine(pen, 3, heightI - 4, widthI - 4, 3);
                gI.Save();
                gI.Dispose();
                document = null;
            }

            Graphics g = Graphics.FromImage(cardImg);

            if (location.x.Unit == CardUnits.percentage)
            {
                x = (cardImg.Width * x) / 100;
            }
            if (location.y.Unit == CardUnits.percentage)
            {
                y = (cardImg.Height * y) / 100;
            }




            if (width < 1)
            {
                setWidth = upImg.Width;
            }
            else
            {
                if (size.x.Unit == CardImgUnits.percentage)
                {
                    setWidth = (cardImg.Width * setWidth) / 100;
                }
                else if (size.x.Unit == CardImgUnits.imgPerc)
                {
                    setWidth = (upImg.Width * setWidth) / 100;
                }
            }
            if (height < 1)
            {
                setHeight = upImg.Height;
                if (size.x.Unit == CardImgUnits.percentage)
                {
                    if (size.x.Value > 0)
                    {
                        setHeight = (setWidth / upImg.Width) * upImg.Height;
                    }
                }
                else if (size.x.Unit == CardImgUnits.imgPerc)
                {
                    if (size.x.Value > 0)
                    {
                        setHeight = (setWidth / upImg.Width) * upImg.Height;
                    }
                }

            }
            else
            {
                if (size.y.Unit == CardImgUnits.percentage)
                {
                    setHeight = (cardImg.Height * setHeight) / 100;
                    if (size.x.Value == 0)
                        setWidth = (setHeight / upImg.Height) * upImg.Width;
                }
                else if (size.y.Unit == CardImgUnits.imgPerc)
                {
                    setHeight = (upImg.Height * setHeight) / 100;
                    if (size.x.Value == 0)
                        setWidth = (setHeight / upImg.Height) * upImg.Width;
                }
            }

            if (document != null)
            {
                //document.zoo
                //SizeF siz = new SizeF(setWidth, setHeight);
                //document.RasterizeDimensions(ref siz, (int)setWidth, (int)setHeight);
               
                float ratio = (setWidth / upImg.Width) * 100;
                float ratio2 = (setHeight / upImg.Height) * 100;

                if (ratio > ratio2)
                {
                    document.Width = setWidth;
                    document.Height = (setWidth / upImg.Width) * upImg.Height;
                }
                else
                {
                    document.Height = setHeight;
                    document.Width = (setHeight / upImg.Height) * upImg.Width;
                }
                //document.Width = ratio;
                //document.Height = ratio;

               

                upImg = document.Draw();
            }

            
            if (alingment == Align.right)
            {
                x -= setWidth;
            } else if (alingment == Align.center)
            {
                x -= setWidth / 2;
            }

            if (alignmentHeigth == Align.right)
            {
                y -= setHeight;
            }
            else if (alignmentHeigth == Align.center)
            {
                y -= setHeight / 2;
            }

            g.TranslateTransform(x, y);
            //g.TranslateTransform(-setWidth / 2, -setHeight / 2);
            g.RotateTransform(angle);
            //g.TranslateTransform(-setWidth / 2, -setHeight / 2);

            //int dx = (int)(x * (Math.Cos((angle / 360.0) * 2 * Math.PI)) - (y * (Math.Sin((angle / 360.0) * 2 * Math.PI))));
            //int dy = (int)(x * (Math.Sin((angle / 360.0) * 2 * Math.PI)) - (y * (Math.Cos((angle / 360.0) * 2 * Math.PI))));
            //int dy = 0;

            g.DrawImage(upImg, 0, 0, setWidth, setHeight);
            
            g.Save();
            g.Dispose();
            upImg.Dispose();
        }

        public void DrawShape(Shape shape, Pen pen, Brush brush, TPoint location, TPoint size, int angle = 0, int startAngle = 0, int endAngle = 90, Align alignment = Align.left, Align alignHeight = Align.left, TPointValue? round = null)
        {
            Graphics g = Graphics.FromImage(cardImg);                   // load card image
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            TPointValue Round = round ?? new TPointValue(0, CardUnits.px);
            // ----- Create & recalculate points -----
            float x, y, width, height, roundVal;

            if (location.x.Unit == CardUnits.percentage)
                x = (cardImg.Width * location.x.Value) / 100;
            else
                x = location.x.Value;
            if (location.y.Unit == CardUnits.percentage)
                y = (cardImg.Height * location.y.Value) / 100;
            else
                y = location.y.Value;

            if (size.x.Unit == CardUnits.percentage)
                width = (cardImg.Width * size.x.Value) / 100;
            else
                width = size.x.Value;
            if (size.y.Unit == CardUnits.percentage)
                height = (cardImg.Height * size.y.Value) / 100;
            else
                height = size.y.Value;
            if (Round.Unit == CardUnits.percentage)
                roundVal = (cardImg.Height * Round.Value) / 100;
            else
                roundVal = Round.Value;


            g.TranslateTransform(x, y);
            g.RotateTransform(angle);

            if (alignment == Align.center)
                x = -width / 2;
            else if (alignment == Align.right)
                x = -width;
            else
                x = 0;
            if (alignHeight == Align.center)
                y = -height / 2;
            else if (alignHeight == Align.right)
                y = -height;
            else
                y = 0;



            switch (shape)
            {
                case Shape.rectangle:
                    if (roundVal > 0)
                    {
                        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.Default;

                        g.FillRectangle(brush, x + roundVal, y, width - roundVal*2, height);
                        g.FillRectangle(brush, x , y + roundVal, width, height - roundVal*2);

                        g.FillPie(brush, x, y, roundVal*2, roundVal * 2, 180, 90);
                        g.FillPie(brush, x, y + height - roundVal * 2, roundVal * 2, roundVal * 2, 90, 90);
                        g.FillPie(brush, x + width - roundVal * 2, y, roundVal * 2, roundVal * 2, 270, 90);
                        g.FillPie(brush, x + width - roundVal*2, y + height - roundVal * 2, roundVal * 2, roundVal * 2, 0, 90);

                        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                        g.DrawLine(pen, x + roundVal, y, x + width - roundVal, y);
                        g.DrawLine(pen, x + roundVal, y + height, x + width - roundVal, y + height);
                        g.DrawLine(pen, x, y + roundVal, x, y + height - roundVal);
                        g.DrawLine(pen, x + width, y + roundVal, x + width, y + height - roundVal);

                        g.DrawArc(pen, x, y, roundVal * 2, roundVal * 2, 180, 90);
                        g.DrawArc(pen, x, y + height - roundVal * 2, roundVal * 2, roundVal * 2, 90, 90);
                        g.DrawArc(pen, x + width - roundVal * 2, y, roundVal * 2, roundVal * 2, 270, 90);
                        g.DrawArc(pen, x + width - roundVal * 2, y + height - roundVal * 2, roundVal * 2, roundVal * 2, 0, 90);
                    } 
                    else
                    {
                        g.FillRectangle(brush, x, y, width, height);
                        g.DrawRectangle(pen, x, y, width, height);
                    }
                    break;
                case Shape.ellipse:
                    g.FillEllipse(brush, x, y, width, height);
                    g.DrawEllipse(pen, x, y, width, height);
                    break;
                case Shape.arc:
                    g.FillPie(brush, x, y, width, height, startAngle, endAngle);
                    g.DrawArc(pen, x, y, width, height, startAngle, endAngle);
                    break;
                case Shape.pie:
                    g.FillPie(brush, x, y, width, height, startAngle, endAngle);
                    g.DrawPie(pen, x, y, width, height, startAngle, endAngle);
                    break;
            }


            /*g.DrawLines()
            if (xUnits == CardUnits.percentage)
            {
                x = (cardImg.Width * x) / 100;
            }
            if (yUnits == CardUnits.percentage)
            {
                y = (cardImg.Height * y) / 100;
            }

            int setWidth = width, setHeight = height;*/
            /*if (width < 1)
            {
                setWidth = upImg.Width;
            }
            else
            {
                if (wUnits == CardImgUnits.percentage)
                {
                    setWidth = (cardImg.Width * setWidth) / 100;
                }
                else if (wUnits == CardImgUnits.imgPerc)
                {
                    setWidth = (upImg.Width * setWidth) / 100;
                }
            }
            if (height < 1)
            {
                setHeight = upImg.Height;
            }
            else
            {
                if (hUnits == CardImgUnits.percentage)
                {
                    setHeight = (cardImg.Height * setHeight) / 100;
                }
                else if (hUnits == CardImgUnits.imgPerc)
                {
                    setHeight = (upImg.Height * setHeight) / 100;
                }
            }*/
            /*if (alignment == Align.right)
            {
                x -= setWidth;
            }
            else if (alignment == Align.center)
            {
                x -= setWidth / 2;
            }*/

            // g.DrawImage(upImg, 0, 0, setWidth, setHeight);
            g.Save();
            g.Dispose();

        }

        public void DrawCurve(Curve curve, Pen pen, Brush brush, TPoint[] points, int angle = 0, int endAngle = 90, Align alignment = Align.left, Align alignHeight = Align.left, TPointValue? round = null)
        {
            Graphics g = Graphics.FromImage(cardImg);                   // load card image

            if (points == null || points.Length == 0) return;                             // if no points -> exit

            // ----- Create & recalculate points -----
            float x, y;
            PointF[] locPoints = new PointF[points.Length];
            for (int i = 0; i < points.Length; i++)
            {
                if (points[i].x.Unit == CardUnits.percentage)
                    x = (cardImg.Width * points[i].x.Value) / 100;
                else
                    x = points[i].x.Value;
                if (points[i].y.Unit == CardUnits.percentage)
                    y = (cardImg.Width * points[i].y.Value) / 100;
                else
                    y = points[i].y.Value;
                locPoints[i] = new PointF(x, y);
            }


        }

        public void Save(string fileName)
        {
            cardImg.Save(fileName, ImageFormat.Png);
        }
    }
}
