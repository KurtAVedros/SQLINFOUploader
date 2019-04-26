using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

// -------------------------------------------------------
// Created by Kurt A. Vedros
// 4/7/2018
// Program created to shorten the time it takes to write the SQL program
// -------------------------------------------------------

namespace ISHS_SQL_Shortcut
{
    public partial class Form1 : Form
    {
        //----------------------------------------Variables------------------------------------------------------
        int TempStep7indicator = 0;
        int TempStep8indicator = 0;

        // variables for step 1
        string AccessionNumber = "NULL";
        string CategoryID = "NULL";
        string SubCategoryID = "NULL";
        string Circa = "0";
        string MaterialID = "NULL";
        string SecondaryMaterialID = "NULL";
        int CollectionID = 0;
        string RediscovRecordID = "NULL";
        bool IsOnExhibit = false;
        int SpecimenID = 0;
        byte[] data = null;

        // variables for step 2
        string MediaFilePath = "";
        string MediaFileName = "";
        string MediaFileEnding = "jpg";
        int MediaDataID = 0;

        // variables for step 4
        string ThumbnailFilePath = "";
        string ThumbnailFileName = "";
        string ThumbnailFileEnding = "jpg";
        int ThumbnailDataID = 0;

        // variables for step 5
        string MimeType = "image/jpeg";
        int ImageHeight = 0;
        int ImageWidth = 0;
        string Description = "";
        bool IsSpecimenShowcaseMedia = false;
        int MediaID = 0;

        // variables for step 7
        string TileFilePath = "";
        string TileFileName = "";
        string TileFileEnding = "jpg";

        //int CurrentSetTileFileID = 0;
        int ImageTileDataID = 0;

        // variables for step 8
        int Level = 0;
        int Column = 0;
        int Row = 0;
        //int Step8CurrentSetTileFileID = 0;
        int Step8ImageTileDataID = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        //----------------------------------------Button Click------------------------------------------------------

        private void btnNewSpecimen_Click(object sender, EventArgs e)
        {
            DAL.setDatabase(0);
            if(cbxBase.Checked == true)
            {
                fillInformationBase();
                addToDatabaseCycleBase();
            } 
            if(cbxOne.Checked == true)
            {
                fillInformationOne();
                addToDatabaseCycleAdditional();
            }
            if(cbxTwo.Checked == true)
            {
                fillInformationTwo();
                addToDatabaseCycleAdditional();
            }
            if(cbxThree.Checked == true)
            {
                fillInformationThree();
                addToDatabaseCycleAdditional();
            }
            if (cbxFour.Checked == true)
            {
                fillInformationFour();
                addToDatabaseCycleAdditional();
            }
            if (cbxFive.Checked == true)
            {
                fillInformationFive();
                addToDatabaseCycleAdditional();
            }
            rtbInformation.Text += SpecimenID + " has been added to LOCAL.\n";
        }


        private void btnAddActual_Click(object sender, EventArgs e)
        {
            DAL.setDatabase(1);
            if (cbxBase.Checked == true)
            {
                fillInformationBase();
                addToDatabaseCycleBase();
            }
            if (cbxOne.Checked == true)
            {
                fillInformationOne();
                addToDatabaseCycleAdditional();
            }
            if (cbxTwo.Checked == true)
            {
                fillInformationTwo();
                addToDatabaseCycleAdditional();
            }
            if (cbxThree.Checked == true)
            {
                fillInformationThree();
                addToDatabaseCycleAdditional();
            }
            if (cbxFour.Checked == true)
            {
                fillInformationFour();
                addToDatabaseCycleAdditional();
            }
            if (cbxFive.Checked == true)
            {
                fillInformationFive();
                addToDatabaseCycleAdditional();
            }
            rtbInformation.Text += SpecimenID + " has been added to ACTUAL.\n";
        }

        private void btnClearInfo_Click(object sender, EventArgs e)
        {
            clearText();
            resetCheckBoxs();
        }

        private void cycleThroughImagePathAddingImages()
        {
            // variables limiters
            int levelLimit = 0;
            int columnLimit = 0;
            int rowLimit = 0;
            // variables for count
            int level = 0;
            int column = 0;
            int row = 0;
            // variables for image.
            string image = "";

            //level cycler
            while (levelLimit != 1)
            {
                if (columnLimit == 1)
                {
                    level++;
                    image = TileFilePath + "\\" + level + "\\" + column + "_" + row + "." + TileFileEnding;
                    if (File.Exists(image))
                    {
                        columnLimit = 0;
                        rowLimit = 0;
                    }
                    else
                    {
                        levelLimit = 1;
                    }
                }
                else
                {
                    // column cycler
                    while (columnLimit != 1)
                    {
                        if (rowLimit == 1)
                        {
                            column++;
                            image = TileFilePath + "\\" + level + "\\" + column + "_" + row + "." + TileFileEnding;
                            if (File.Exists(image))
                            {
                                rowLimit = 0;
                            }
                            else
                            {
                                column = 0;
                                columnLimit = 1;
                            }
                        }
                        else
                        {
                            // row cycler
                            while (rowLimit != 1)
                            {
                                image = TileFilePath + "\\" + level + "\\" + column + "_" + row + "." + TileFileEnding;
                                if (File.Exists(image))
                                {
                                    data = System.IO.File.ReadAllBytes(image);
                                    DAL.TileAdd(SpecimenID, MediaID, level, column, row, data);
                                    row++;
                                }
                                else
                                {
                                    row = 0;
                                    rowLimit = 1;
                                }
                            }
                        }
                    }
                }
            }
        }


        //----------------------------------------SQL Code fill------------------------------------------------------
        /*
        private string SQLCreateNewSpeciment()
        {
            string ans = "-- STEP 1 ADD NEW SPECIMEN\n";
            ans += "INSERT INTO Specimens\n";
            ans += "VALUES (" + AccessionNumber + ", " + CategoryID + ", " + SubCategoryID;
            ans += ", " + Circa + ", " + MaterialID + ", " + SecondaryMaterialID + ", ";
            ans += CollectionID + ", " + RediscovRecordID + ", " + IsOnExhibit + ")\n";
            ans += "-- Given SpecimenID of " + SpecimenID;
            ans += "\n\n";

            return ans;
        }

        private string SQLCreateNewMediaData()
        {
            string ans = "-- STEP 2 ADD NEW MEDIADATA\n";
            ans += "INSERT INTO MediaData(DATA)\n";
            ans += "SELECT * FROM OPENROWSET (BULK N'" + MediaFilePath + "\\" + MediaFileName;
            ans += "." + MediaFileEnding + "', SINGLE_BLOB) rset\n";
            ans += "-- Given MediaDataID of " + MediaDataID;
            ans += "\n\n";

            return ans;
        }

        private string SQLStep3Reminder()
        {
            string ans = "-- STEP 3 CREATE THUMBNAIL\n";
            ans += "-- Done with GIMP or Paint";
            ans += "\n\n";

            return ans;
        }

        private string SQLCreateNewThumbnailData()
        {
            string ans = "-- STEP 4 ADD NEW ThumbnailDATA\n";
            ans += "INSERT INTO MediaData(DATA)\n";
            ans += "SELECT * FROM OPENROWSET (BULK N'" + ThumbnailFilePath + "\\" + ThumbnailFileName;
            ans += "." + ThumbnailFileEnding + "', SINGLE_BLOB) rset\n";
            ans += "-- Thumbnail has a Given MediaDataID of " + ThumbnailDataID;
            ans += "\n\n";

            return ans;
        }

        private string SQLCreateNewMedia()
        {
            string ans = "-- STEP 5 ADD MediaData and ThumbnailData to Media\n";
            ans += "INSERT INTO Media(SpecimenID, FileName, Extension, MimeType,";
            ans += "MediaDataID, ThumbnailDataID, Height, Width, AltText, IsSpecimenShowcaseMedia)\n";
            ans += "VALUES (" + SpecimenID + ", '" + MediaFileName + "." + MediaFileEnding + "', '";
            ans += MediaFileEnding + "', '" + MimeType + "', " + MediaDataID + ", " + ThumbnailDataID;
            ans += ", " + ImageHeight + ", " + ImageWidth + ", '" + Description + "', " + IsSpecimenShowcaseMedia + ")\n";
            ans += "-- Media has a Given MediaID of " + MediaID;
            ans += "\n\n";

            return ans;
        }

        private string SQLStep6Reminder()
        {
            string ans = "-- STEP 6 CREATE Image Cut UP\n";
            ans += "-- Done with Deep Zoom Composer";
            ans += "\n\n";

            return ans;
        }

        private string SQLAddNewImageTileData()
        {
            string ans = "";
            if(TempStep7indicator == 0)
            {
                ans += "-- STEP 7 ADD Image Tiles to ImageTileData\n";
                TempStep7indicator = 1;
            }
            ans += "INSERT INTO ImageTileData(Image)\n";
            ans += "SELECT * FROM OPENROWSET(BULK N'" + TileFilePath + "\\" + TileFileName;
            ans += "." + TileFileEnding + "', SINGLE_BLOB) rset\n";
            ans += "-- ImageTileDataID has a Given MediaDataID of " + ImageTileDataID;
            ans += "\n\n";

            setNextImageTileDataID();

            return ans;
        }

        private string SQLAddNewImageTile()
        {
            string ans = "";
            if (TempStep8indicator == 0)
            {
                ans += "-- STEP 8 ADD MetaData to ImageTile\n";
                TempStep8indicator = 1;
            }
            ans += "INSERT INTO ImageTiles(SpecimenID, Level, [Column], Row, ImageTileDataID, MediaID)\n";
            ans += "VALUES (" + SpecimenID + ", " + Level + ", " + Column + ", " + Row + ", ";
            ans += Step8ImageTileDataID + ", " + MediaID +")";

            ans += "\n\n";

            Step8setNextImageTileDataID();

            return ans;
            
        }
        */
        //----------------------------------------Infromation Fill------------------------------------------------------
        private void fillInformationBase()
        {
            // for Specimen
            AccessionNumber = tbxAccessionNum.Text;
            setCategory();
            setSubCategory();
            Circa = tbxCirca.Text;
            MaterialID = "NULL";
            SecondaryMaterialID = "NULL";
            setCollection();
            RediscovRecordID = tbxRediscovRecordID.Text;
            setIsOnExhibit();
            //SpecimenID = int.Parse(tbxSpecimenID.Text);

            // for MediaData
            MediaFilePath = tbxMediaPath.Text;
            MediaFileName = tbxMediaName.Text;
            //  MediaFileEnding = "jpg";
            //MediaDataID = int.Parse(tbxNextMediaDataID.Text);

            // for ThumbnailData
            ThumbnailFilePath = tbxThumPath.Text;
            ThumbnailFileName = tbxMediaName.Text;
            //  ThumbnailFileEnding = "jpg";
            //ThumbnailDataID = int.Parse(tbxNextMediaDataID.Text) + 1;

            // for Media
            //  MimeType = "image/jpeg";
            string file = MediaFilePath + "//" + MediaFileName + "." + MediaFileEnding;
            System.Drawing.Image img = System.Drawing.Image.FromFile(@file);
            ImageHeight = img.Height;
            ImageWidth = img.Width;
            Description = tbxDescription.Text;
            IsSpecimenShowcaseMedia = true;
            //MediaID = int.Parse(tbxNextMediaID.Text);

            TileFilePath = tbxTileFilePath.Text;
            //TileFileName = tbxTileName.Text;
            //  TileFileEnding = "jpg";
        }

        private void fillInformationOne()
        {
            // for MediaData
            //MediaFilePath = tbxMediaPath1.Text;
            MediaFileName = tbxMediaName1.Text;
            //  MediaFileEnding = "jpg";
            //MediaDataID = int.Parse(tbxNextMediaDataID.Text);

            // for ThumbnailData
            //ThumbnailFilePath = tbxThumPath1.Text;
            ThumbnailFileName = tbxMediaName1.Text;
            //  ThumbnailFileEnding = "jpg";
            //ThumbnailDataID = int.Parse(tbxNextMediaDataID.Text) + 1;

            // for Media
            //  MimeType = "image/jpeg";
            string file = MediaFilePath + "//" + MediaFileName + "." + MediaFileEnding;
            System.Drawing.Image img = System.Drawing.Image.FromFile(@file);
            ImageHeight = img.Height;
            ImageWidth = img.Width;
            Description = tbxDescription1.Text;
            IsSpecimenShowcaseMedia = false;
            //MediaID = int.Parse(tbxNextMediaID.Text);

            TileFilePath = tbxTileFilePath1.Text;
            //TileFileName = tbxTileName.Text;
            //  TileFileEnding = "jpg";
        }

        private void fillInformationTwo()
        {
            // for MediaData
            //MediaFilePath = tbxMediaPath2.Text;
            MediaFileName = tbxMediaName2.Text;
            //  MediaFileEnding = "jpg";
            //MediaDataID = int.Parse(tbxNextMediaDataID.Text);

            // for ThumbnailData
            //ThumbnailFilePath = tbxThumPath2.Text;
            ThumbnailFileName = tbxMediaName2.Text;
            //  ThumbnailFileEnding = "jpg";
            //ThumbnailDataID = int.Parse(tbxNextMediaDataID.Text) + 2;

            // for Media
            //  MimeType = "image/jpeg";
            string file = MediaFilePath + "//" + MediaFileName + "." + MediaFileEnding;
            System.Drawing.Image img = System.Drawing.Image.FromFile(@file);
            ImageHeight = img.Height;
            ImageWidth = img.Width;
            Description = tbxDescription2.Text;
            IsSpecimenShowcaseMedia = false;
            //MediaID = int.Parse(tbxNextMediaID.Text);

            TileFilePath = tbxTileFilePath2.Text;
            //TileFileName = tbxTileName.Text;
            //  TileFileEnding = "jpg";
        }

        private void fillInformationThree()
        {
            // for MediaData
            //MediaFilePath = tbxMediaPath3.Text;
            MediaFileName = tbxMediaName3.Text;
            //  MediaFileEnding = "jpg";
            //MediaDataID = int.Parse(tbxNextMediaDataID.Text);

            // for ThumbnailData
            //ThumbnailFilePath = tbxThumPath3.Text;
            ThumbnailFileName = tbxMediaName3.Text;
            //  ThumbnailFileEnding = "jpg";
            //ThumbnailDataID = int.Parse(tbxNextMediaDataID.Text) + 3;

            // for Media
            //  MimeType = "image/jpeg";
            string file = MediaFilePath + "//" + MediaFileName + "." + MediaFileEnding;
            System.Drawing.Image img = System.Drawing.Image.FromFile(@file);
            ImageHeight = img.Height;
            ImageWidth = img.Width;
            Description = tbxDescription3.Text;
            IsSpecimenShowcaseMedia = false;
            //MediaID = int.Parse(tbxNextMediaID.Text);

            TileFilePath = tbxTileFilePath3.Text;
            //TileFileName = tbxTileName.Text;
            //  TileFileEnding = "jpg";
        }

        private void fillInformationFour()
        {
            // for MediaData
            // MediaFilePath = tbxMediaPath4.Text;
            MediaFileName = tbxMediaName4.Text;
            //  MediaFileEnding = "jpg";
            //MediaDataID = int.Parse(tbxNextMediaDataID.Text);

            // for ThumbnailData
            // ThumbnailFilePath = tbxThumPath4.Text;
            ThumbnailFileName = tbxMediaName4.Text;
            //  ThumbnailFileEnding = "jpg";
            //ThumbnailDataID = int.Parse(tbxNextMediaDataID.Text) + 4;

            // for Media
            //  MimeType = "image/jpeg";
            string file = MediaFilePath + "//" + MediaFileName + "." + MediaFileEnding;
            System.Drawing.Image img = System.Drawing.Image.FromFile(@file);
            ImageHeight = img.Height;
            ImageWidth = img.Width;
            Description = tbxDescription4.Text;
            IsSpecimenShowcaseMedia = false;
            //MediaID = int.Parse(tbxNextMediaID.Text);

            TileFilePath = tbxTileFilePath4.Text;
            //TileFileName = tbxTileName.Text;
            //  TileFileEnding = "jpg";
        }

        private void fillInformationFive()
        {
            // for MediaData
            // MediaFilePath = tbxMediaPath5.Text;
            MediaFileName = tbxMediaName5.Text;
            //  MediaFileEnding = "jpg";
            //MediaDataID = int.Parse(tbxNextMediaDataID.Text);

            // for ThumbnailData
            // ThumbnailFilePath = tbxThumPath5.Text;
            ThumbnailFileName = tbxMediaName5.Text;
            //  ThumbnailFileEnding = "jpg";
            //ThumbnailDataID = int.Parse(tbxNextMediaDataID.Text) + 5;

            // for Media
            //  MimeType = "image/jpeg";
            string file = MediaFilePath + "//" + MediaFileName + "." + MediaFileEnding;
            System.Drawing.Image img = System.Drawing.Image.FromFile(@file);
            ImageHeight = img.Height;
            ImageWidth = img.Width;
            Description = tbxDescription5.Text;
            IsSpecimenShowcaseMedia = false;
            //MediaID = int.Parse(tbxNextMediaID.Text);

            TileFilePath = tbxTileFilePath5.Text;
            //TileFileName = tbxTileName.Text;
            //  TileFileEnding = "jpg";
        }

        // Cycles for adding information into the database
        private void addToDatabaseCycleBase()
        {
            int SubCategoryNum = 0;
            if (SubCategoryID != "NULL")
                SubCategoryNum = int.Parse(SubCategoryID);
            SpecimenID = DAL.SpecimenAdd(0, AccessionNumber, int.Parse(CategoryID), SubCategoryNum, Circa,
                CollectionID, IsOnExhibit, int.Parse(RediscovRecordID));
            string DataMedia = MediaFilePath + "\\" + MediaFileName + "." + MediaFileEnding;
            data = System.IO.File.ReadAllBytes(DataMedia);
            MediaDataID = DAL.MediaDataAdd(0, data);
            string DataThumbnail = ThumbnailFilePath + "\\" + ThumbnailFileName + "." + ThumbnailFileEnding;
            data = System.IO.File.ReadAllBytes(DataThumbnail);
            ThumbnailDataID = DAL.MediaDataAdd(0, data);
            MediaID = DAL.MediaAdd(SpecimenID, MediaFileName + "." + MediaFileEnding, MediaFileEnding, MimeType, MediaDataID, ThumbnailDataID, ImageHeight, ImageWidth, Description, IsSpecimenShowcaseMedia);

            cycleThroughImagePathAddingImages();
        }

        private void addToDatabaseCycleAdditional()
        {
            string DataMedia = MediaFilePath + "\\" + MediaFileName + "." + MediaFileEnding;
            data = System.IO.File.ReadAllBytes(DataMedia);
            MediaDataID = DAL.MediaDataAdd(0, data);
            string DataThumbnail = ThumbnailFilePath + "\\" + ThumbnailFileName + "." + ThumbnailFileEnding;
            data = System.IO.File.ReadAllBytes(DataThumbnail);
            ThumbnailDataID = DAL.MediaDataAdd(0, data);
            MediaID = DAL.MediaAdd(SpecimenID, MediaFileName + "." + MediaFileEnding, MediaFileEnding, MimeType, MediaDataID, ThumbnailDataID, ImageHeight, ImageWidth, Description, IsSpecimenShowcaseMedia);

            cycleThroughImagePathAddingImages();
        }


        //----------------------------------------Other Methods------------------------------------------------------
        private void setNextImageTileDataID()
        {
            ImageTileDataID = ImageTileDataID + 1;
            //tbxSetImageTileDataID.Text = ImageTileDataID.ToString();
        }

        private void Step8setNextImageTileDataID()
        {
            Step8ImageTileDataID = Step8ImageTileDataID + 1;
            //tbxStep8SetImageTileDataID.Text = Step8ImageTileDataID.ToString();
        }

        private void clearText()
        {
            tbxAccessionNum.Clear();
            tbxCirca.Clear();
            tbxCollection.Clear();
            tbxCategory.Clear();
            tbxSubCategory.Clear();
            tbxRediscovRecordID.Clear();

            tbxDescription.Clear();
            tbxDescription1.Clear();
            tbxDescription2.Clear();
            tbxDescription3.Clear();
            tbxDescription4.Clear();
            tbxDescription5.Clear();

            tbxMediaPath.Clear();

            tbxThumPath.Clear();

            tbxTileFilePath.Clear();
            tbxTileFilePath1.Clear();
            tbxTileFilePath2.Clear();
            tbxTileFilePath3.Clear();
            tbxTileFilePath4.Clear();
            tbxTileFilePath5.Clear();

            tbxMediaName.Clear();
            tbxMediaName1.Clear();
            tbxMediaName2.Clear();
            tbxMediaName3.Clear();
            tbxMediaName4.Clear();
            tbxMediaName5.Clear();
        }

        private void resetCheckBoxs()
        {
            if (cbxBase.Checked == true)
                cbxBase.Checked = false;
            if (cbxOne.Checked == true)
                cbxOne.Checked = false;
            if (cbxTwo.Checked == true)
                cbxTwo.Checked = false;
            if (cbxThree.Checked == true)
                cbxThree.Checked = false;
            if (cbxFour.Checked == true)
                cbxFour.Checked = false;
            if (cbxFive.Checked == true)
                cbxFive.Checked = false;
        }

        private void setIsOnExhibit()
        {
            if ( tbxIsOnExibit.Text.ToLower() == "no")
            { IsOnExhibit = false; }
            else if (tbxIsOnExibit.Text.ToLower() == "0")
            { IsOnExhibit = false; }
            else if (tbxIsOnExibit.Text.ToLower() == "yes")
            { IsOnExhibit = true; }
            else if (tbxIsOnExibit.Text.ToLower() == "1")
            { IsOnExhibit = true; }
            else
            { IsOnExhibit = false; }
        }

        private void setCollection()
        {
            if (tbxCollection.Text == "1870")
            { CollectionID = 1; }
            else if (tbxCollection.Text == "1910")
            { CollectionID = 2; }
            else if (tbxCollection.Text == "1880")
            { CollectionID = 3; }
            else if (tbxCollection.Text == "1940")
            { CollectionID = 4; }
            else if (tbxCollection.Text == "1920")
            { CollectionID = 5; }
            else if (tbxCollection.Text == "1890")
            { CollectionID = 7; }
            else if (tbxCollection.Text == "1900")
            { CollectionID = 8; }
            else if (tbxCollection.Text == "1980")
            { CollectionID = 9; }
            else if (tbxCollection.Text == "1960")
            { CollectionID = 10; }
            else if (tbxCollection.Text == "1970")
            { CollectionID = 11; }
            else if (tbxCollection.Text == "1950")
            { CollectionID = 12; }
            else if (tbxCollection.Text == "1830")
            { CollectionID = 13; }
            else if (tbxCollection.Text == "Unknown")
            { CollectionID = 14; }
            else if (tbxCollection.Text == "1930")
            { CollectionID = 15; }
            else if (tbxCollection.Text == "1850")
            { CollectionID = 16; }
            else if (tbxCollection.Text == "1860")
            { CollectionID = 17; }
            else if (tbxCollection.Text == "1990")
            { CollectionID = 18; }
            else if (tbxCollection.Text == "1840")
            { CollectionID = 19; }
            else if (tbxCollection.Text == "Currentlyon Exhibit")
            { CollectionID = 20; }
            else if (tbxCollection.Text.ToLower() == "unassigned")
            { CollectionID = 21; }
            else if (tbxCollection.Text.ToLower() == "un")
            { CollectionID = 21; }
            else
            { tbxCollection.Text = "ERROR"; }
        }

        private void setCategory()
        {
            if (tbxCategory.Text == "Women's Garments")
            { CategoryID = "1"; }
            else if (tbxCategory.Text == "WG")
            { CategoryID = "1"; }
            else if (tbxCategory.Text == "Men's Garments")
            { CategoryID = "2"; }
            else if (tbxCategory.Text == "MG")
            { CategoryID = "2"; }
            else if (tbxCategory.Text == "Children's Garments")
            { CategoryID = "3"; }
            else if (tbxCategory.Text == "CG")
            { CategoryID = "3"; }
            else if (tbxCategory.Text == "Military Uniforms")
            { CategoryID = "4"; }
            else if (tbxCategory.Text == "MU")
            { CategoryID = "4"; }
            else if (tbxCategory.Text == "Men's Footwear")
            { CategoryID = "5"; }
            else if (tbxCategory.Text == "MF")
            { CategoryID = "5"; }
            else if (tbxCategory.Text == "Women's Footwear")
            { CategoryID = "6"; }
            else if (tbxCategory.Text == "WF")
            { CategoryID = "6"; }
            else if (tbxCategory.Text == "Personal Gear")
            { CategoryID = "7"; }
            else if (tbxCategory.Text == "PG")
            { CategoryID = "7"; }
            else if (tbxCategory.Text == "Wedding Dress")
            { CategoryID = "8"; }
            else if (tbxCategory.Text == "WD")
            { CategoryID = "8"; }
            else if (tbxCategory.Text == "Formalwear")
            { CategoryID = "9"; }
            else if (tbxCategory.Text == "FW")
            { CategoryID = "9"; }
            else if (tbxCategory.Text == "Hats")
            { CategoryID = "10"; }
            else if (tbxCategory.Text == "H")
            { CategoryID = "10"; }
            else if (tbxCategory.Text == "Military Uniform/Hat")
            { CategoryID = "11"; }
            else if (tbxCategory.Text == "MU/H")
            { CategoryID = "11"; }
            else if (tbxCategory.Text == "Purses")
            { CategoryID = "12"; }
            else if (tbxCategory.Text == "P")
            { CategoryID = "12"; }
            else if (tbxCategory.Text == "Daywear")
            { CategoryID = "13"; }
            else if (tbxCategory.Text == "DW")
            { CategoryID = "13"; }
            else if (tbxCategory.Text.ToLower() == "null")
            { CategoryID = "NULL"; }
            else
            { tbxCategory.Text = "ERROR"; }
        }

        private void setSubCategory()
        {
            if (tbxSubCategory.Text == "Women's Garments")
            { SubCategoryID = "1"; }
            else if (tbxSubCategory.Text == "WG")
            { SubCategoryID = "1"; }
            else if (tbxSubCategory.Text == "Men's Garments")
            { SubCategoryID = "2"; }
            else if (tbxSubCategory.Text == "MG")
            { SubCategoryID = "2"; }
            else if (tbxSubCategory.Text == "Children's Garments")
            { SubCategoryID = "3"; }
            else if (tbxSubCategory.Text == "CG")
            { SubCategoryID = "3"; }
            else if (tbxSubCategory.Text == "Military Uniforms")
            { SubCategoryID = "4"; }
            else if (tbxSubCategory.Text == "MU")
            { SubCategoryID = "4"; }
            else if (tbxSubCategory.Text == "Men's Footwear")
            { SubCategoryID = "5"; }
            else if (tbxSubCategory.Text == "MF")
            { SubCategoryID = "5"; }
            else if (tbxSubCategory.Text == "Women's Footwear")
            { SubCategoryID = "6"; }
            else if (tbxSubCategory.Text == "WF")
            { SubCategoryID = "6"; }
            else if (tbxSubCategory.Text == "Personal Gear")
            { SubCategoryID = "7"; }
            else if (tbxSubCategory.Text == "PG")
            { SubCategoryID = "7"; }
            else if (tbxSubCategory.Text == "Wedding Dress")
            { SubCategoryID = "8"; }
            else if (tbxSubCategory.Text == "WD")
            { SubCategoryID = "8"; }
            else if (tbxSubCategory.Text == "Formalwear")
            { SubCategoryID = "9"; }
            else if (tbxSubCategory.Text == "FW")
            { SubCategoryID = "9"; }
            else if (tbxSubCategory.Text == "Hats")
            { SubCategoryID = "10"; }
            else if (tbxSubCategory.Text == "H")
            { SubCategoryID = "10"; }
            else if (tbxSubCategory.Text == "Military Uniform/Hat")
            { SubCategoryID = "11"; }
            else if (tbxSubCategory.Text == "MU/H")
            { SubCategoryID = "11"; }
            else if (tbxSubCategory.Text == "Purses")
            { SubCategoryID = "12"; }
            else if (tbxSubCategory.Text == "P")
            { SubCategoryID = "12"; }
            else if (tbxSubCategory.Text == "Daywear")
            { SubCategoryID = "13"; }
            else if (tbxSubCategory.Text == "DW")
            { SubCategoryID = "13"; }
            else if (tbxSubCategory.Text.ToLower() == "null")
            { SubCategoryID = "0"; }
            else
            { SubCategoryID = "0"; }
        }

        private void cbxBase_CheckedChanged(object sender, EventArgs e)
        {
            if (cbxBase.Checked == false)
            {
                if (cbxOne.Checked == true)
                    cbxOne.Checked = false;
                if (cbxTwo.Checked == true)
                    cbxTwo.Checked = false;
                if (cbxThree.Checked == true)
                    cbxThree.Checked = false;
                if (cbxFour.Checked == true)
                    cbxFour.Checked = false;
                if (cbxFive.Checked == true)
                    cbxFive.Checked = false;
            }
        }

        private void cbxOne_CheckedChanged(object sender, EventArgs e)
        {
            if (cbxOne.Checked == false)
            {
                if (cbxTwo.Checked == true)
                    cbxTwo.Checked = false;
                if (cbxThree.Checked == true)
                    cbxThree.Checked = false;
                if (cbxFour.Checked == true)
                    cbxFour.Checked = false;
                if (cbxFive.Checked == true)
                    cbxFive.Checked = false;
            }
            else if (cbxOne.Checked == true)
            {
                if (cbxBase.Checked == false)
                    cbxBase.Checked = true;
            }
        }

        private void cbxTwo_CheckedChanged(object sender, EventArgs e)
        {
            if (cbxTwo.Checked == false)
            {
                if (cbxThree.Checked == true)
                    cbxThree.Checked = false;
                if (cbxFour.Checked == true)
                    cbxFour.Checked = false;
                if (cbxFive.Checked == true)
                    cbxFive.Checked = false;
            }
            else if (cbxTwo.Checked == true)
            {
                if (cbxBase.Checked == false)
                    cbxBase.Checked = true;
                if (cbxOne.Checked == false)
                    cbxOne.Checked = true;
                if (cbxThree.Checked == true)
                    cbxThree.Checked = false;
                if (cbxFour.Checked == true)
                    cbxFour.Checked = false;
                if (cbxFive.Checked == true)
                    cbxFive.Checked = false;
            }
        }

        private void cbxThree_CheckedChanged(object sender, EventArgs e)
        {
            if (cbxThree.Checked == false)
            {
                if (cbxFour.Checked == true)
                    cbxFour.Checked = false;
                if (cbxFive.Checked == true)
                    cbxFive.Checked = false;
            }
            else if (cbxThree.Checked == true)
            {
                if (cbxBase.Checked == false)
                    cbxBase.Checked = true;
                if (cbxOne.Checked == false)
                    cbxOne.Checked = true;
                if (cbxTwo.Checked == false)
                    cbxTwo.Checked = true;
            }
        }

        private void cbxFour_CheckedChanged(object sender, EventArgs e)
        {
            if (cbxFour.Checked == false)
            {
                if (cbxFive.Checked == true)
                    cbxFive.Checked = false;
            }
            else if (cbxFour.Checked == true)
            {
                if (cbxBase.Checked == false)
                    cbxBase.Checked = true;
                if (cbxOne.Checked == false)
                    cbxOne.Checked = true;
                if (cbxTwo.Checked == false)
                    cbxTwo.Checked = true;
                if (cbxThree.Checked == false)
                    cbxThree.Checked = true;
            }
        }

        private void cbxFive_CheckedChanged(object sender, EventArgs e)
        {
            if (cbxFive.Checked == true)
            {
                if (cbxBase.Checked == false)
                    cbxBase.Checked = true;
                if (cbxOne.Checked == false)
                    cbxOne.Checked = true;
                if (cbxTwo.Checked == false)
                    cbxTwo.Checked = true;
                if (cbxThree.Checked == false)
                    cbxThree.Checked = true;
                if (cbxFour.Checked == false)
                    cbxFour.Checked = true;
            }
        }
    }
}
