using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using myFunctions;
using CardGen;

namespace CardGenIDE
{
    

    public partial class frmMainIDE : Form
    {
        CardDeck deck;

        public frmMainIDE()
        {
            InitializeComponent();
        }

        private void frmMainIDE_Load(object sender, EventArgs e)
        {

            deck = new CardDeck(59, 88, 300);
            txtCardWidth.Text = deck.cardSet.width.ToString();
            txtCardHeight.Text = deck.cardSet.height.ToString();
            cbPaper.Text = "A4";

            if (Set.Default.LoadLastDeck)
            {
                if (Set.Default.LastDeckPath != "")
                {
                    deck.LoadDeck(Set.Default.LastDeckPath);
                    refreshDeckInfo();
                }
            }
        }

        private void refreshDeckInfo()
        {
            this.Text = "CargGenIDE - " + System.IO.Path.GetFileNameWithoutExtension(deck.ScriptFileName);
            txtCardWidth.Text = deck.cardSet.width.ToString();
            txtCardHeight.Text = deck.cardSet.height.ToString();
            txtDPI.Text = deck.cardSet.dpi.ToString();

            chbGenerateBack.Checked = deck.genSet.genBackCards;
            chbGenTextWord.Checked = deck.genSet.genTextInDoc;
            if (deck.genSet.genType == GenType.docx)
                cbGenType.SelectedIndex = 0;
            else if (deck.genSet.genType == GenType.png)
                cbGenType.SelectedIndex = 1;
            txtDataFile.Text = deck.DataFileName;
            cbPaper.Text = deck.genSet.genPaper.ToString();
            chbLandscape.Checked = deck.genSet.genPapLandskape;
            ShowCard();
            ShowBackCard();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmSelItem formSelItem = new frmSelItem();
            CardObjectType objectType = CardObjectType.label;
            if (formSelItem.ShowDialog(ref objectType) == DialogResult.OK)
            {
                int selItem = -1;
                if (objectType == CardObjectType.image)
                {
                    selItem = deck.CardFore.AddImage();
                }
                else if (objectType == CardObjectType.text)
                {
                    selItem = deck.CardFore.AddText();
                }
                else if (objectType == CardObjectType.shape)
                {
                    selItem = deck.CardFore.AddShape();
                }
                else
                {
                    selItem = deck.CardFore.AddLabel();
                }
                
                ShowCard(selItem);
            }
        }

        public void ShowCard(int sel = -1)
        {
            int lastIndex = cbObjects.SelectedIndex;
            cbObjects.Items.Clear();
            List<string> objList = deck.CardFore.GetObjectList();
            cbObjects.Items.AddRange(objList.ToArray());

            cardImg.Image = deck.CardFore.GetImage();

            if (sel >= 0)
            {
                cbObjects.SelectedIndex = sel;
                propGrid.SelectedObject = deck.CardFore.GetItem(sel);
            } else
            {
                if (cbObjects.Items.Count > 0)
                {
                    if (lastIndex < cbObjects.Items.Count && lastIndex >= 0)
                        cbObjects.SelectedIndex = lastIndex;
                    else
                        cbObjects.SelectedIndex = 0;
                } else
                {
                    propGrid.SelectedObject = null;
                }
                
            }
                
        }

        public void ShowBackCard(int sel = -1)
        {
            int lastIndex = cbObjectsB.SelectedIndex;
            cbObjectsB.Items.Clear();
            List<string> objList = deck.CardBack.GetObjectList();
            cbObjectsB.Items.AddRange(objList.ToArray());

            cardImgBack.Image = deck.CardBack.GetImage();

            if (sel >= 0)
            {
                cbObjectsB.SelectedIndex = sel;
                propGridB.SelectedObject = deck.CardBack.GetItem(sel);
            }
            else
            {
                if (cbObjectsB.Items.Count > 0)
                {
                    if (lastIndex < cbObjectsB.Items.Count && lastIndex >= 0)
                        cbObjectsB.SelectedIndex = lastIndex;
                    else
                        cbObjectsB.SelectedIndex = 0;
                }
                else
                {
                    propGridB.SelectedObject = null;
                }

            }

        }

        private void propGrid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            ShowCard();
        }

        private void cbObjects_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbObjects.SelectedIndex >= 0)
            {
                int sel = cbObjects.SelectedIndex;
                propGrid.SelectedObject = deck.CardFore.GetItem(sel);
            }
            else
            {
                propGrid.SelectedObject = null;
            }
        }

        private void propGrid_Click(object sender, EventArgs e)
        {

        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if (cbObjects.SelectedIndex >= 0)
            {
                deck.CardFore.DelItem(cbObjects.SelectedIndex);
                ShowCard();
            }
            
        }

        private void mnuExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void mnuNew_Click(object sender, EventArgs e)
        {
            deck = new CardDeck();
            ShowCard();
            ShowBackCard();
        }

        private void mnuOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Generate Playing Cards Deck Script | *.gdc";
            if (System.IO.Directory.Exists(Set.Default.LastDeckPath))
                dialog.InitialDirectory = System.IO.Path.GetDirectoryName(Set.Default.LastDeckPath);
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                Set.Default.LastDeckPath = dialog.FileName;
                deck.LoadDeck(dialog.FileName);

                refreshDeckInfo();
            }
        }

        private void mnuSave_Click(object sender, EventArgs e)
        {
            if (deck.ScriptFileName != "")
            {
                deck.SaveDeck();
            } else
            {
                mnuSaveAs_Click(sender, e);
            }
           
        }

        private void mnuSaveAs_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Generate Playing Cards Deck Script | *.gdc";
            if (deck.ScriptFileName != "")
            {
                dialog.FileName = System.IO.Path.GetFileName(deck.ScriptFileName);
                dialog.InitialDirectory = System.IO.Path.GetDirectoryName(deck.ScriptFileName);

            }
            else if (System.IO.Directory.Exists(Set.Default.LastDeckPath))
                dialog.InitialDirectory = System.IO.Path.GetDirectoryName(Set.Default.LastDeckPath);
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                Set.Default.LastDeckPath = dialog.FileName;
                deck.SaveDeck(dialog.FileName);
            }

        }

        private void mnuClose_Click(object sender, EventArgs e)
        {
            
        }

        private void mnuGenerate_Click(object sender, EventArgs e)
        {
            deck.Generate();
        }

        private void cbObjectsB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbObjectsB.SelectedIndex >= 0)
            {
                int sel = cbObjectsB.SelectedIndex;
                propGridB.SelectedObject = deck.CardBack.GetItem(sel);
            }
            else
            {
                propGridB.SelectedObject = null;
            }
        }

        private void btnAddB_Click(object sender, EventArgs e)
        {
            frmSelItem formSelItem = new frmSelItem();
            CardObjectType objectType = CardObjectType.label;
            if (formSelItem.ShowDialog(ref objectType) == DialogResult.OK)
            {
                int selItem = -1;
                if (objectType == CardObjectType.image)
                {
                    selItem = deck.CardBack.AddImage();
                }
                else if (objectType == CardObjectType.text)
                {
                    selItem = deck.CardBack.AddText();
                }
                else if (objectType == CardObjectType.shape)
                {
                    selItem = deck.CardBack.AddShape();
                }
                else
                {
                    selItem = deck.CardBack.AddLabel();
                }

                ShowBackCard(selItem);
            }
        }

        private void btnDelB_Click(object sender, EventArgs e)
        {
            if (cbObjectsB.SelectedIndex >= 0)
            {
                deck.CardBack.DelItem(cbObjectsB.SelectedIndex);
                ShowBackCard();
            }
        }

        private void propGridB_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            ShowBackCard();
        }

        private void frmMainIDE_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Set.Default.LastDeckPath = deck.FileName;

            Set.Default.Save();
        }

        private void btnSetCardSize_Click(object sender, EventArgs e)
        {
            try
            {
                deck.SetCardSize(Convert.ToInt32(txtCardWidth.Text), Convert.ToInt32(txtCardHeight.Text), Convert.ToInt32(txtDPI.Text));
            }
            catch (Exception)
            {
                Dialogs.ShowErr("Wrong Value!", "Error");
            }
            
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            deck.Generate();
        }

        private void chbGenerateBack_CheckedChanged(object sender, EventArgs e)
        {
            deck.genSet.genBackCards = chbGenerateBack.Checked;
        }

        private void chbGenTextWord_CheckedChanged(object sender, EventArgs e)
        {
            deck.genSet.genTextInDoc = chbGenTextWord.Checked;
        }

        private void cbGenType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbGenType.SelectedIndex >= 0)
            {
                if (cbGenType.SelectedIndex == 0)
                    deck.genSet.genType = GenType.docx;
                else if (cbGenType.SelectedIndex == 1)
                    deck.genSet.genType = GenType.png;
            }
        }

        private void cbPaper_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbPaper.SelectedIndex >= 0)
            {
                try
                {
                    deck.genSet.genPaper = (WordEditor.PageSizes)Enum.Parse(typeof(WordEditor.PageSizes), cbPaper.Text, true);
                }
                catch
                {
                    deck.genSet.genPaper = WordEditor.PageSizes.A4;
                }
            }
        }

        private void chbLandscape_CheckedChanged(object sender, EventArgs e)
        {
            deck.genSet.genPapLandskape = chbLandscape.Checked;
        }

        private void btnBackUp_Click(object sender, EventArgs e)
        {
            if (cbObjectsB.SelectedIndex > 0)
            {
                TCardObject item = deck.CardBack.itemList[cbObjectsB.SelectedIndex];
                deck.CardBack.DelItem(cbObjectsB.SelectedIndex);
                deck.CardBack.InsertItem(cbObjectsB.SelectedIndex - 1, item);
                ShowBackCard();

                cbObjectsB.SelectedIndex = cbObjectsB.SelectedIndex - 1;
            }
        }

        private void btnBackDown_Click(object sender, EventArgs e)
        {
            if (cbObjectsB.SelectedIndex >= 0 && (cbObjectsB.SelectedIndex + 1) < cbObjectsB.Items.Count)
            {
                TCardObject item = deck.CardBack.itemList[cbObjectsB.SelectedIndex];
                deck.CardBack.DelItem(cbObjectsB.SelectedIndex);
                deck.CardBack.InsertItem(cbObjectsB.SelectedIndex + 1, item);
                ShowBackCard();

                cbObjectsB.SelectedIndex = cbObjectsB.SelectedIndex + 1;
            }
        }

        private void btnForeUp_Click(object sender, EventArgs e)
        {
            if (cbObjects.SelectedIndex > 0)
            {
                TCardObject item = deck.CardFore.itemList[cbObjects.SelectedIndex];
                deck.CardFore.DelItem(cbObjects.SelectedIndex);
                deck.CardFore.InsertItem(cbObjects.SelectedIndex - 1, item);
                ShowCard();
                cbObjects.SelectedIndex = cbObjects.SelectedIndex - 1;
            }
        }

        private void btnForeDown_Click(object sender, EventArgs e)
        {
            if (cbObjects.SelectedIndex >= 0 && (cbObjects.SelectedIndex + 1) < cbObjects.Items.Count)
            {
                TCardObject item = deck.CardFore.itemList[cbObjects.SelectedIndex];
                deck.CardFore.DelItem(cbObjects.SelectedIndex);
                deck.CardFore.InsertItem(cbObjects.SelectedIndex + 1, item);
                ShowCard();
                cbObjects.SelectedIndex = cbObjects.SelectedIndex + 1;
            }
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            if (cbObjects.SelectedIndex >= 0)
            {
                TCardObject item = new TCardObject();
                item = deck.CardFore.itemList[cbObjects.SelectedIndex];
                deck.CardFore.AddItem(item);
                ShowCard();
                cbObjects.SelectedIndex = cbObjects.Items.Count - 1;
            }
        }

        private void btnCopyB_Click(object sender, EventArgs e)
        {
            if (cbObjectsB.SelectedIndex >= 0)
            {
                TCardObject item = deck.CardBack.itemList[cbObjectsB.SelectedIndex];
                deck.CardBack.AddItem(item);
                ShowBackCard();
                cbObjectsB.SelectedIndex = cbObjectsB.Items.Count - 1;
            }
        }

        private void mnuAbout_Click(object sender, EventArgs e)
        {
            frmAbout about = new frmAbout();
            about.ShowDialog();
        }
    }
}
