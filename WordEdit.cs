using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
//using System.IO.Packaging;

using DocumentFormat.OpenXml.Wordprocessing;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml;

using A = DocumentFormat.OpenXml.Drawing;
using DW = DocumentFormat.OpenXml.Drawing.Wordprocessing;
using PIC = DocumentFormat.OpenXml.Drawing.Pictures;
using System.Drawing;
using DocumentFormat.OpenXml.Drawing.Charts;

// v1.0.0

namespace WordEditor
{
    /// <summary>
    /// Namespace for Word Editor classes based on OpenXML library
    /// </summary>
    static class NamespaceDoc{}
    
    public enum PageSizes
    {
        User, A0, A1, A2, A3, A4, A5, A6, A7, A8
    }

    public struct TPageSettings
    {
        public PageSizes PageType;
        public int Width;
        public int Height;
        public bool Landscape;
    }

    public struct TImgResult
    {
        public string ID;
        public Int64Value width;
        public Int64Value height;
    }

    public struct TextBoxProps
    {
        public float x;
        public float y;
        public float width;
        public float height;
        public string text;
        public int angle;
        public int alignment;
        public System.Drawing.Font font;
        public System.Drawing.Color color;
    }

    /// <summary>
    /// Segment Class - part of Paragraph
    /// </summary>
    public class Segment
    {
        /// <summary>
        /// Reference to Run
        /// </summary>
        Run r;

        /// <summary>
        /// Gets or sets text in segment
        /// </summary>
        public string Text
        {
            get
            {
                return r.InnerText;
                
            }
            set
            {
                Text t;
                // ----- If no text item -> create -----
                if (r.Elements<Text>().Count() == 0)
                {
                    t = new Text(Text);         // create text item
                    t.Space = SpaceProcessingModeValues.Preserve;  // no trim spaces
                    r.Append(t);                // add text to Run
                }
                // ----- Change Text -----
                t = r.Elements<Text>().First();
                t.Text = Text;
            }
        }

        /// <summary>
        /// Clear style and formatting of segment
        /// </summary>
        public void ClearFormat()
        {
            // ----- Clear styles -----
            while (r.Elements<RunProperties>().Count() > 0)
            {
                r.Elements<RunProperties>().First().Remove();
            }
        }

        /// <summary>
        /// Set style of segment
        /// </summary>
        /// <param name="style">Name of Segment style</param>
        public void SetStyle(string style)
        {
            ClearFormat();              // Clear styles

            // ----- Set new style -----
            if (style != "")
            {
                RunProperties r_pr = new RunProperties();
                r_pr.RunStyle = new RunStyle() { Val = style };
                r.PrependChild<RunProperties>(r_pr);
            }
        }

        /// <summary>
        /// Get style of segment
        /// </summary>
        /// <returns>String name of style</returns>
        public string GetStyle()
        {
            string res = "";
            if (r.Elements<RunProperties>().Count() > 0)
            {
                RunProperties r_pr = r.Elements<RunProperties>().First();
                if (r_pr.Elements<RunStyle>().Count() > 0)
                    res = r_pr.RunStyle.Val;
            }
            return res;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="r">Reference to Run in OpenXML doc</param>
        public Segment(Run r)
        {
            this.r = r;
        }

    }

    /// <summary>
    /// Segment Collection Class
    /// </summary>
    public class SegmentCollection : System.Collections.CollectionBase
    {
        /// <summary>
        /// Reference to Paragraph
        /// </summary>
        Paragraph p;

        /// <summary>
        /// Constructor - add reference to Paragraph in OpenXML
        /// </summary>
        /// <param name="p">Reference to Paragraph in OpenXML</param>
        public SegmentCollection(Paragraph p)
        {
            this.p = p;
        }

        /// <summary>
        /// Internal Function. Adding Segment item to SegmentCollection while Creating Collection Tree.
        /// </summary>
        /// <param name="segItem"></param>
        internal void _add(Segment segItem)
        {
            List.Add(segItem);
        }

        /// <summary>
        /// Adding Segment to Paragraph
        /// </summary>
        /// <param name="text">Segment text</param>
        /// <param name="style">Name of Segment style</param>
        public void Add(string text, string style = "")
        {
            // ----- Create new Segement item -----
            Run r = new Run();      // new Run
            Text t = new Text(text);// new Text
            t.Space = SpaceProcessingModeValues.Preserve;  // no trim spaces
            r.Append(t);            // add Text to Run

            // ----- Set new style -----
            if (style != "")
            {
                RunProperties r_pr = new RunProperties();
                r_pr.RunStyle = new RunStyle() { Val = style };
                r.PrependChild<RunProperties>(r_pr);
            }

            // ----- Add Segment to Paragraph -----
            Segment segItem = new Segment(r);
            p.Append(r);
            List.Add(segItem);
        }

        /// <summary>
        /// Remove Segment from Paragraph
        /// </summary>
        /// <param name="index">Segment index</param>
        public void Remove(int index)
        {
            // Check if item exist
            if (index > Count - 1 || index < 0)
            {
                System.Windows.Forms.MessageBox.Show("Index not valid!");
            }
            else
            {
                p.Elements<Run>().ElementAt(index).Remove();
                List.RemoveAt(index);
            }
        }

        /// <summary>
        /// Return segment item by index
        /// </summary>
        /// <param name="Index">Segment index</param>
        /// <returns>Segment item</returns>
        public Segment Item(int Index)
        {
            // The appropriate item is retrieved from the List object and
            // explicitly cast to the Widget type, then returned to the 
            // caller.
            return (Segment)List[Index];
        }
    }

    /// <summary>
    /// Paragraph Class
    /// </summary>
    public class Paragraf
    {
        /// <summary>
        /// Reference to OpenXML Paragraph
        /// </summary>
        private Paragraph p;

        /// <summary>
        /// Segment collection in Paragraph
        /// </summary>
        public SegmentCollection Segments;

        /// <summary>
        /// Get paragraph text
        /// </summary>
        public string Text
        {
            get
            {
                return p.InnerText;
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="p">Reference to OpenXML Paragraph</param>
        /// <param name="segCollection">Segment Collection</param>
        public Paragraf(Paragraph p, SegmentCollection segCollection)
        {
            this.p = p;
            this.Segments = segCollection;
        }

        /// <summary>
        /// Clear all Segments in Paragraph (clear all text)
        /// </summary>
        public void Clear()
        {
            // ----- clear paragraph -----
            int count = p.Elements<Run>().Count();
            for (int j = 0; j < count; j++)
            {
                p.Elements<Run>().First().Remove();
            }
            Segments.Clear();
        }

        /// <summary>
        /// Clear style and formatting of paragraph
        /// </summary>
        public void ClearFormat()
        {
            // ----- clear paragraph format -----
            while (p.Elements<ParagraphProperties>().Count() > 0)
            {
                p.Elements<ParagraphProperties>().First().Remove();
            }
        }

        /// <summary>
        /// Set style of Paragraph
        /// </summary>
        /// <param name="style">Name of paragraph style</param>
        public void SetStyle(string style)
        {
            ClearFormat();      // clear formatting
            if (style != "")    // create new style
            {
                ParagraphProperties pPr = new ParagraphProperties();
                pPr.ParagraphStyleId = new ParagraphStyleId() { Val = style };
                p.PrependChild<ParagraphProperties>(pPr);
            }
        }

        /// <summary>
        /// Get style of Paragraph
        /// </summary>
        /// <returns>Name of paragraph style</returns>
        public string GetStyle()
        {
            string res = "";
            if (p.Elements<ParagraphProperties>().Count() > 0)
            {
                ParagraphProperties pPr = p.Elements<ParagraphProperties>().First();
                if (pPr.Elements<ParagraphStyleId>().Count() > 0)
                    res = pPr.ParagraphStyleId.Val;
            }
            return res;
        }

    }

    /// <summary>
    /// Paragraph Collection Class
    /// </summary>
    public class ParagrafCollection : System.Collections.CollectionBase
    {
        /// <summary>
        /// Reference to OpenXML document
        /// </summary>
        private WordprocessingDocument doc;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="doc">Reference to OpenXML document</param>
        public ParagrafCollection(WordprocessingDocument doc)
        {
            this.doc = doc;
        }

        /// <summary>
        /// Internal Function. Adding Paragraph item to ParagraphCollection while Creating Collection Tree.
        /// </summary>
        /// <param name="parItem">Paragraph item</param>
        internal void _add(Paragraf parItem)
        {
            List.Add(parItem);
        }


        private Paragraph newParagraph(string text, string style = "")
        {
            // ----- Create new paragraph -----
            Run r = new Run();      // new Run
            Text t = new Text(text);// new Text
            t.Space = SpaceProcessingModeValues.Preserve;  // no trim spaces
            r.Append(t);            // add Text to Run
            Paragraph p = new Paragraph();
            p.Append(r);

            // ----- Set new style -----
            if (style != "")    // create new style
            {
                ParagraphProperties pPr = new ParagraphProperties();
                pPr.ParagraphStyleId = new ParagraphStyleId() { Val = style };
                p.PrependChild<ParagraphProperties>(pPr);
            }

            return p;
        }

        private Paragraf _newParagraf(Paragraph p)
        {
            Segment segItem;
            Paragraf parItem;
            SegmentCollection segCol = new SegmentCollection(p);

            int partCount = p.Elements<Run>().Count();
            for (int j = 0; j < partCount; j++)
            {
                Run tr = p.Elements<Run>().ElementAt(j);
                segItem = new Segment(tr);
                segCol._add(segItem);
            }
            parItem = new Paragraf(p, segCol);
            return parItem;
        }

        /// <summary>
        /// Adding Paragraph to document
        /// </summary>
        /// <param name="text">Segment text</param>
        /// <param name="style">Name of Segment style</param>
        public void Add(string text, string style = "")
        {
            // ----- Create new Paragraph -----
            Paragraph p = newParagraph(text, style);
            doc.MainDocumentPart.Document.Body.AppendChild(p);

            // ----- Create new Paragraf -----
            Paragraf parItem = _newParagraf(p);    // create new Paragraf
            List.Add(parItem);

        }

        

        public void FillTableCell(int tableIndex, int colIndex, int rowIndex, TextBoxProps tbp)
        {
            Table table = doc.MainDocumentPart.Document.Body.Elements<Table>().ElementAt(tableIndex);

            // Find the second row in the table.
            TableRow row = table.Elements<TableRow>().ElementAt(rowIndex);

            // Find the third cell in the row.
            TableCell cell = row.Elements<TableCell>().ElementAt(colIndex);

            // Find the first paragraph in the table cell.
            Paragraph p = cell.Elements<Paragraph>().First();

            Picture pict = new Picture();
            DocumentFormat.OpenXml.Vml.Shape s = new DocumentFormat.OpenXml.Vml.Shape() { Style = "position:absolute;top:" + (tbp.y * 3.79).ToString() +  ";left:" + (tbp.x * 4).ToString() + ";width:" + (tbp.width*3.79).ToString() + ";height:" + (tbp.height * 3.79).ToString() + ";margin-left:0pt;margin-top:0pt;rotation:0;v-rotate-letters:true;mso-rotate:90", Filled = false, Stroked = false };
            //DocumentFormat.OpenXml.Vml.Shapetype st = new DocumentFormat.OpenXml.Vml.Shapetype();
            //st.

            DocumentFormat.OpenXml.Vml.TextBox tb = new DocumentFormat.OpenXml.Vml.TextBox() { Inset = "0,0,0,0" };
            Paragraph par = new Paragraph();
            ParagraphProperties paragraphProperties = new ParagraphProperties();
            SpacingBetweenLines spacing = new SpacingBetweenLines() { Line = "240", LineRule = LineSpacingRuleValues.Auto, Before = "0", After = "0" };
            paragraphProperties.Append(spacing);
            paragraphProperties.Append(new ParagraphStyleId() { Val = "No Spacing" });
            par.Append(paragraphProperties);
            Run r = new Run();
            RunProperties rPr = new RunProperties(new RunFonts(){   HighAnsi = tbp.font.Name, Ascii = tbp.font.Name, ComplexScript = tbp.font.Name }, new FontSize() { Val = ((int)(tbp.font.Size * 2)).ToString() }, new DocumentFormat.OpenXml.Wordprocessing.Color() { Val = tbp.color.R.ToString("X2") + tbp.color.G.ToString("X2") + tbp.color.B.ToString("X2") });  // fontsize in half-points
            r.PrependChild<RunProperties>(rPr);
            r.Append(new Text(tbp.text));
            par.Append(r);
            TextBoxContent tbc = new TextBoxContent();
            tbc.Append(par);
            tb.Append(tbc);
            s.Append(tb);
            pict.Append(s);
            p.Append(new Run(pict));
            

            // Set the text for the run.
            //Text t = r.Elements<Text>().First();
            //t.Text = txt;
        }

        public void FillTableCell(int tableIndex, int colIndex, int rowIndex, TImgResult img)
        {
            Table table = doc.MainDocumentPart.Document.Body.Elements<Table>().ElementAt(tableIndex);

            // Find the second row in the table.
            TableRow row = table.Elements<TableRow>().ElementAt(rowIndex);

            // Find the third cell in the row.
            TableCell cell = row.Elements<TableCell>().ElementAt(colIndex);

            // Find the first paragraph in the table cell.
            Paragraph p = cell.Elements<Paragraph>().First();
            

            // Define the reference of the image.
            var element =
                 new Drawing(
                     new DW.Inline(
                         new DW.Extent() { Cx = img.width, Cy = img.height },
                         new DW.EffectExtent()
                         {
                             LeftEdge = 0L,
                             TopEdge = 0L,
                             RightEdge = 0L,
                             BottomEdge = 0L
                         },
                         new DW.DocProperties()
                         {
                             Id = (UInt32Value)1U,
                             Name = "Picture 1"
                         },
                         new DW.NonVisualGraphicFrameDrawingProperties(
                             new A.GraphicFrameLocks() { NoChangeAspect = true }),
                         new A.Graphic(
                             new A.GraphicData(
                                 new PIC.Picture(
                                     new PIC.NonVisualPictureProperties(
                                         new PIC.NonVisualDrawingProperties()
                                         {
                                             Id = (UInt32Value)0U,
                                             Name = "New Bitmap Image.png"
                                         },
                                         new PIC.NonVisualPictureDrawingProperties()),
                                     new PIC.BlipFill(
                                         new A.Blip(
                                             new A.BlipExtensionList(
                                                 new A.BlipExtension()
                                                 {
                                                     Uri =
                                                        "{28A0092B-C50C-407E-A947-70E740481C1C}"
                                                 })
                                         )
                                         {
                                             Embed = img.ID,
                                             CompressionState =
                                             A.BlipCompressionValues.Print
                                         },
                                         new A.Stretch(
                                             new A.FillRectangle())),
                                     new PIC.ShapeProperties(
                                         new A.Transform2D(
                                             new A.Offset() { X = 0L, Y = 0L },
                                             new A.Extents() { Cx = img.width, Cy = img.height }),
                                         new A.PresetGeometry(
                                             new A.AdjustValueList()
                                         )
                                         {
                                             Preset = A.ShapeTypeValues.Rectangle
                                         }))
                             )
                             {
                                 Uri = "http://schemas.openxmlformats.org/drawingml/2006/picture"
                             })
                     )
                     {
                         DistanceFromTop = (UInt32Value)0U,
                         DistanceFromBottom = (UInt32Value)0U,
                         DistanceFromLeft = (UInt32Value)0U,
                         DistanceFromRight = (UInt32Value)0U,
                         EditId = "50D07946"
                     });

            

            
            // Find the first run in the paragraph.
            Run r = p.Elements<Run>().First();

            r.Append(element);

            // Set the text for the run.
            //Text t = r.Elements<Text>().First();
            //t.Text = txt;
        }

        /// <summary>
        /// Adding Table to document
        /// </summary>
        /// <param name="text">Segment text</param>
        /// <param name="style">Name of Segment style</param>
        public void AddTable(int Cols, int Rows, int cellHeight = 0, int cellWidth = 0)
        {
            // Create an empty table.
            Table table = new Table();

            // Create a TableProperties object and specify its border information.
            TableProperties tblProp = new TableProperties(
                new TableBorders(
                    new TopBorder() { Val = new EnumValue<BorderValues>(BorderValues.None), Size = 1 },
                    new BottomBorder() { Val = new EnumValue<BorderValues>(BorderValues.None), Size = 1 },
                    new LeftBorder() { Val = new EnumValue<BorderValues>(BorderValues.None), Size = 1 },
                    new RightBorder() { Val = new EnumValue<BorderValues>(BorderValues.None), Size = 1 },
                    new InsideHorizontalBorder() { Val = new EnumValue<BorderValues>(BorderValues.None), Size = 1 },
                    new InsideVerticalBorder() { Val = new EnumValue<BorderValues>(BorderValues.None), Size = 1 }
                ),
                new TableJustification() { Val = TableRowAlignmentValues.Center }
            );
            // Append the TableProperties object to the empty table.
            table.AppendChild<TableProperties>(tblProp);

            TableRow tr;
            TableCell tc;

            for (int i = 0; i < Rows; i++)
            {
                if (cellHeight > 0)
                    tr = new TableRow(new TableRowProperties(new TableRowHeight() { Val = (UInt32Value)(cellHeight * 50.8 + 1) }));
                else
                    tr = new TableRow();
                tr.Append();

                for (int j = 0; j < Cols; j++)
                {
                    tc = new TableCell();
                    // Specify the width property of the table cell.
                    if (cellWidth > 0)
                        tc.Append(new TableCellProperties(new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = (cellWidth * 50.8 + 1).ToString() }));
                    else
                        tc.Append(new TableCellProperties(new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = "2400" }));

                    SpacingBetweenLines spacing = new SpacingBetweenLines() { Line = "240", LineRule = LineSpacingRuleValues.Auto, Before = "0", After = "0" };
                    ParagraphProperties paragraphProperties = new ParagraphProperties();
                    Paragraph paragraph = new Paragraph();
                    paragraphProperties.Append(spacing);
                    paragraphProperties.Append(new ParagraphStyleId() { Val = "No Spacing" });
                    paragraph.Append(paragraphProperties);
                    paragraph.Append(new Run(new Text("")));
                    

                    tc.Append(paragraph);
                    tr.Append(tc);
                }
                table.Append(tr);
            }
            doc.MainDocumentPart.Document.Body.Append(table);
            // Specify the table cell content.
            //tc1.Append(new Paragraph(new Run(new Text("Hello, World!"))));
            //tc1.Append(new Paragraph(new Run(element)));


            // ------------
            // Create a second table cell by copying the OuterXml value of the first table cell.
            //TableCell tc2 = new TableCell(tc1.OuterXml);
            

            // ----- Create new Paragraph -----
            /*Paragraph p = newParagraph(text, style);

            Table t = new Table()

            doc.MainDocumentPart.Document.Body.AppendChild(p);

            // ----- Create new Paragraf -----
            Paragraf parItem = _newParagraf(p);    // create new Paragraf
            List.Add(parItem);*/

        }

        /// <summary>
        /// Adding Paragraph to document
        /// </summary>
        /// <param name="index">Segment index for inserting</param>
        /// <param name="text">Segment text</param>
        /// <param name="style">Name of Segment style</param>
        public void Insert(int index, string text, string style = "")
        {
            // ----- Create new Paragraph -----
            Paragraph p = newParagraph(text, style);
            if (index < 0)
                doc.MainDocumentPart.Document.Body.PrependChild(p);
            else if (index >= doc.MainDocumentPart.Document.Body.Elements<Paragraph>().Count())
                doc.MainDocumentPart.Document.Body.AppendChild(p);
            else
                doc.MainDocumentPart.Document.Body.InsertAt(p, index);

            // ----- Create new Paragraf -----
            Paragraf parItem = _newParagraf(p);    // create new Paragraf
            if (index < 0)
                if (List.Count > 0) List.Insert(0, parItem);
                else List.Add(parItem);
            else if (index >= doc.MainDocumentPart.Document.Body.Elements<Paragraph>().Count())
                List.Add(parItem);
            else
                List.Insert(index, parItem);
        }

        /// <summary>
        /// Remove Paragraph from document
        /// </summary>
        /// <param name="index">Paragraf index</param>
        public void Remove(int index)
        {
            // Check if item exist
            if (index > Count - 1 || index < 0)
            {
                System.Windows.Forms.MessageBox.Show("Index not valid!");
            }
            else
            {
                Paragraph p = doc.MainDocumentPart.Document.Body.Elements<Paragraph>().ElementAt(index);
                p.Remove();
                List.RemoveAt(index);
            }
        }

        /// <summary>
        /// Return Paragraph Item
        /// </summary>
        /// <param name="Index">Paragraph Index</param>
        /// <returns>Paragraph Item</returns>
        public Paragraf Item(int Index)
        {
            return (Paragraf)List[Index];
        }
    }

    /// <summary>
    /// Word (.docx) Editor Class based on OpenXML Library
    /// </summary>
    [Serializable()]
    public class WordEdit
    {
        private bool isOpen = false;
        private WordprocessingDocument doc;
        private string filename;
        private MemoryStream stream;

        /// <summary>
        /// Paragraphs in OpenXML
        /// </summary>
        public ParagrafCollection Paragraphs;


        /// <summary>
        /// Constructor (If Filename -> try open file)
        /// </summary>
        /// <param name="fileName">File name of docx document</param>
        /// <exception cref="System.ArgumentNullException">The exception that is thrown when a null reference is passed to a method that does not accept it as a valid argument.</exception>
        /// <exception cref="DocumentFormat.OpenXml.Packaging.OpenXmlPackageException">OpenXml exception class</exception>
        public WordEdit(string fileName = "")
        {
            
            if (fileName != "")
            {
                if (doc != null) doc.Close();
                isOpen = _open(fileName);
            }
        }

        /// <summary>
        /// Open .docx File
        /// </summary>
        /// <param name="fileName">File name of .docx document</param>
        /// <exception cref="System.ArgumentNullException">The exception that is thrown when a null reference is passed to a method that does not accept it as a valid argument.</exception>
        /// <exception cref="DocumentFormat.OpenXml.Packaging.OpenXmlPackageException">OpenXml exception class</exception>
        public void Open(string fileName)
        {
            try
            {
                if (doc != null) doc.Close();
            }
            catch (Exception)
            {
            }
            
            isOpen = _open(fileName);
        }


        /// <summary>
        /// Create .docx File
        /// </summary>
        /// <param name="fileName">File name of .docx document</param>
        /// <exception cref="System.ArgumentNullException">The exception that is thrown when a null reference is passed to a method that does not accept it as a valid argument.</exception>
        /// <exception cref="DocumentFormat.OpenXml.Packaging.OpenXmlPackageException">OpenXml exception class</exception>
        public bool Create(string fileName)
        {
            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(fileName));
                File.Delete(fileName);
            }
            catch (Exception) { }

            if (doc != null) doc.Close();
            TPageSettings set = GetPageSize(PageSizes.A4, false);
            isOpen = _create(fileName, set);
            return isOpen;
        }

        /// <summary>
        /// Create .docx File
        /// </summary>
        /// <param name="fileName">File name of .docx document</param>
        /// <param name="PageSize">New Page Size</param>
        /// <exception cref="System.ArgumentNullException">The exception that is thrown when a null reference is passed to a method that does not accept it as a valid argument.</exception>
        /// <exception cref="DocumentFormat.OpenXml.Packaging.OpenXmlPackageException">OpenXml exception class</exception>
        public bool Create(string fileName, TPageSettings PageSize)
        {
            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(fileName));
                File.Delete(fileName);
            }
            catch (Exception) { }
            
            if (doc != null) doc.Close();
            isOpen = _create(fileName, PageSize);
            return isOpen;
        }


        /// <summary>
        /// Save .docx File
        /// </summary>
        public void Save()
        {
            if (isOpen)
            {
                _save();
            }
        }

        /// <summary>
        /// Save .docx File As
        /// </summary>
        /// <param name="fileName">Filename for new document</param>
        public void SaveAs(string fileName)
        {
            this.filename = fileName;
            if (isOpen)
            {
                _save();
            }
        }

        /// <summary>
        /// Close and Save .docx file
        /// </summary>
        public void Close()
        {
            if (Paragraphs != null) Paragraphs.Clear();
            if (doc != null)
            {
                doc.Close();
                doc.Dispose();
                doc = null;
            }
            isOpen = false;
        }

        /// <summary>
        /// Add Image object to Word document
        /// </summary>
        /// <param name="stream">Image stream</param>
        /// <param name="type">Image format</param>
        /// <returns></returns>
        public TImgResult AddImage(Stream stream, ImagePartType type)
        {
            TImgResult res = new TImgResult();
            res.ID = "";

            ImagePart imagePart;
            Bitmap bitmap;

            try
            {
                imagePart = doc.MainDocumentPart.AddImagePart(type);
                bitmap = new Bitmap(stream);
                stream.Position = 0;
                imagePart.FeedData(stream);
                stream.Dispose();
            }
            catch (Exception)
            {
                stream.Dispose();
                return res;
            }
           
            res.ID = doc.MainDocumentPart.GetIdOfPart(imagePart);
            res.width = (Int64Value)((bitmap.Width / bitmap.VerticalResolution) * 914400L);
            res.height = (Int64Value)((bitmap.Height / bitmap.HorizontalResolution) * 914400L);

            bitmap.Dispose();
            return res;
        }

        public void SetMargins(float left, float right, float top, float bottom)
        {
           SectionProperties sectionProps = new SectionProperties();
            PageMargin pageMargins1 = new PageMargin();// { Top = 1008, Right =500, Bottom = 1008, Left = (UInt32Value)1008U, Header = (UInt32Value)720U, Footer = (UInt32Value)720U, Gutter = (UInt32Value)0U };
            pageMargins1.Left = (UInt32Value)(left * 50.8);
            pageMargins1.Right = (UInt32Value)(right * 50.8);
            pageMargins1.Top = (Int32Value)(top * 50.8);
            pageMargins1.Bottom = (Int32Value)(bottom * 50.8);


            /*PageMargins pageMargins1 = new PageMargins();
            pageMargins1.Left = 0.1D;
            pageMargins1.Right = 0.1D;
            pageMargins1.Top = 0.1D;
            pageMargins1.Bottom = 0.1D;
            pageMargins1.Header = 0.1D;
            pageMargins1.Footer = 0.1D;*/
            sectionProps.Append(pageMargins1);
            doc.MainDocumentPart.Document.Body.Append(sectionProps);
            //doc.MainDocumentPart.Document.Body.AppendChild(pageMargins1);

            //worksheetPart.Worksheet.AppendChild(pageMargins1);

            /*PageSetup pageSetup = new PageSetup();
            pageSetup.Orientation = OrientationValues.Landscape;
            pageSetup.FitToHeight = 2;
            pageSetup.HorizontalDpi = 200;
            pageSetup.VerticalDpi = 200;
            worksheetPart.Worksheet.AppendChild(pageSetup);*/

        }

        public TPageSettings GetPageSize (PageSizes size, bool Landscape)
        {
            TPageSettings pageSet;
            pageSet.PageType = size;
            pageSet.Landscape = Landscape;
            pageSet.Height = 297;
            pageSet.Width = 210;
            switch (size)
            {
                case PageSizes.A0:
                    pageSet.Height = 1189;
                    pageSet.Width = 841;
                    break;
                case PageSizes.A1:
                    pageSet.Height = 841;
                    pageSet.Width = 594;
                    break;
                case PageSizes.A2:
                    pageSet.Height = 594;
                    pageSet.Width = 420;
                    break;
                case PageSizes.A3:
                    pageSet.Height = 420;
                    pageSet.Width = 297;
                    break;
                
                case PageSizes.A5:
                    pageSet.Height = 210;
                    pageSet.Width = 148;
                    break;
                case PageSizes.A6:
                    pageSet.Height = 148;
                    pageSet.Width = 105;
                    break;
                case PageSizes.A7:
                    pageSet.Height = 105;
                    pageSet.Width = 74;
                    break;
                case PageSizes.A8:
                    pageSet.Height = 74;
                    pageSet.Width = 52;
                    break;
            }

            if (Landscape)
            {
                int x = pageSet.Height;
                pageSet.Height = pageSet.Width;
                pageSet.Width = x;
            }
            return pageSet;
        }

        /// <summary>
        /// Internal open file function
        /// </summary>
        /// <param name="fileName">File name</param>
        /// <returns>Return true of open ok</returns>
        private bool _open(string fileName)
        {
            this.filename = fileName;
            try
            {
                byte[] byteArray = File.ReadAllBytes(fileName);
                stream = new MemoryStream();
                stream.Write(byteArray, 0, (int)byteArray.Length);
                
                doc = WordprocessingDocument.Open(stream, true);
                ParseParagraphs();
                return true;
            }
            catch (Exception err)
            {
                if (doc != null) doc.Close();
                return false;
            }
        }

        /// <summary>
        /// Internal open file function
        /// </summary>
        /// <param name="fileName">File name</param>
        /// <returns>Return true of open ok</returns>
        private bool _create(string fileName, TPageSettings PageSize)
        {
            this.filename = fileName;
            try
            {
                const float SizeConst = 16838.0f / 297.0f;
                //const float SizeConst = 16838/ 297;
                /*byte[] byteArray = File.ReadAllBytes(fileName);
                stream = new MemoryStream();
                stream.Write(byteArray, 0, (int)byteArray.Length);*/
                doc = WordprocessingDocument.Create(fileName,WordprocessingDocumentType.Document);
                doc.AddMainDocumentPart();
                doc.MainDocumentPart.Document = new Document();
                doc.MainDocumentPart.Document.Body = new Body();
                
                SectionProperties SecPro = new SectionProperties();
                PageSize PSize = new PageSize();
                // w:w="11906" w:h="16838"/>
                PSize.Width = (UInt32Value)(SizeConst * PageSize.Width);
                PSize.Height = (UInt32Value)(SizeConst * PageSize.Height);
                SecPro.Append(PSize);

                PageMargin pageMargins1 = new PageMargin();
                pageMargins1.Left = (UInt32Value)(10 * 50.8); // 10mm
                pageMargins1.Right = (UInt32Value)(10 * 50.8);
                pageMargins1.Top = (Int32Value)(10 * 50.8);
                pageMargins1.Bottom = (Int32Value)(10 * 50.8);

                SecPro.Append(pageMargins1);

                doc.MainDocumentPart.Document.Body.Append(SecPro);
                
                ParseParagraphs();
                return true;
            }
            catch (Exception err)
            {
                if (doc != null) doc.Close();
                return false;
            }
        }


        /// <summary>
        /// Internal save file function
        /// </summary>
        /// <returns>Return true of save ok</returns>
        private bool _save()
        {
            try
            {
                if (doc != null)
                {
                    doc.MainDocumentPart.Document.Save();
                    if (stream != null)
                    {
                        File.WriteAllBytes(this.filename, stream.ToArray());
                    }
                }
                return true;
            }
            catch (Exception err)
            {
                return false;
            }
        }

        /// <summary>
        /// Create Paragraph tree from file
        /// </summary>
        private void ParseParagraphs()
        {
            Paragraphs = new ParagrafCollection(doc);
            Segment segItem;
            SegmentCollection segCollection;
            Paragraf parItem;


            int count = doc.MainDocumentPart.Document.Body.Elements<Paragraph>().Count();
            for (int i = 0; i < count; i++)
            {
                Paragraph p = doc.MainDocumentPart.Document.Body.Elements<Paragraph>().ElementAt(i);
                segCollection = new SegmentCollection(p);

                int partCount = p.Elements<Run>().Count();
                for (int j = 0; j < partCount; j++)
                {
                    Run tr = p.Elements<Run>().ElementAt(j);
                    segItem = new Segment(tr);
                    segCollection._add(segItem);
                }
                parItem = new Paragraf(p, segCollection);
                Paragraphs._add(parItem);
            }
        }
    }
}
