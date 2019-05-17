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
        string CommonName = "";

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

        /* Removed adding specimens to a local Database as it became a hassle to update the two databases.
        private void btnNewSpecimen_Click(object sender, EventArgs e)
        {
            if(!CheckInput())
            {
                DAL.setDatabase(0);
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
                rtbInformation.Text += SpecimenID + " has been added to LOCAL.\n";
            }
        }
        */

        private void btnAddActual_Click(object sender, EventArgs e)
        {
            if (!CheckInput())
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
                rtbInformation.Text += SpecimenID + " has been added to the Database.\n";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!CheckInputWithSpecimenID())
            {
                DAL.setDatabase(0);
                if (cbxBase.Checked == true)
                {
                    fillInformationBaseWithSpecimenID();
                    addToDatabaseCycleAdditional();
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
                rtbInformation.Text += "Images has been added to the Database for SpecimenID " + SpecimenID + ".\n";
            }
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
            setCollection(Circa);
            RediscovRecordID = tbxRediscovRecordID.Text;
            setIsOnExhibit();
            CommonName = tbxCommonName.Text;
            //SpecimenID = int.Parse(tbxSpecimenID.Text);

            // for MediaData
            MediaFilePath = tbxMediaPath.Text;
            MediaFileName = tbxMediaName.Text;
            //  MediaFileEnding = "jpg";
            //MediaDataID = int.Parse(tbxNextMediaDataID.Text);

            // for ThumbnailData
            ThumbnailFilePath = tbxMediaPath.Text + "\\Thumbnail";
            ThumbnailFileName = tbxMediaName.Text;
            //  ThumbnailFileEnding = "jpg";
            //ThumbnailDataID = int.Parse(tbxNextMediaDataID.Text) + 1;

            // for Media
            //  MimeType = "image/jpeg";
            string file = MediaFilePath + "//" + MediaFileName + "." + MediaFileEnding;
            System.Drawing.Image img = System.Drawing.Image.FromFile(@file);
            ImageHeight = img.Height;
            ImageWidth = img.Width;
            Description = CommonName + ": " + tbxDescription.Text;
            IsSpecimenShowcaseMedia = true;
            //MediaID = int.Parse(tbxNextMediaID.Text);

            TileFilePath = tbxMediaPath.Text + "\\Tiles\\" + MediaFileName + "_stitch_files";
            //TileFileName = tbxTileName.Text;
            //  TileFileEnding = "jpg";
        }

        private void fillInformationBaseWithSpecimenID()
        {
            // for Specimen
            SpecimenID = int.Parse(tbxSpecimenNumber.Text);

            // for MediaData
            MediaFilePath = tbxMediaPath.Text;
            MediaFileName = tbxMediaName.Text;
            //  MediaFileEnding = "jpg";
            //MediaDataID = int.Parse(tbxNextMediaDataID.Text);

            // for ThumbnailData
            ThumbnailFilePath = tbxMediaPath.Text + "\\Thumbnail";
            ThumbnailFileName = tbxMediaName.Text;
            //  ThumbnailFileEnding = "jpg";
            //ThumbnailDataID = int.Parse(tbxNextMediaDataID.Text) + 1;

            // for Media
            //  MimeType = "image/jpeg";
            string file = MediaFilePath + "//" + MediaFileName + "." + MediaFileEnding;
            System.Drawing.Image img = System.Drawing.Image.FromFile(@file);
            ImageHeight = img.Height;
            ImageWidth = img.Width;
            Description = CommonName + ": " + tbxDescription.Text;
            IsSpecimenShowcaseMedia = true;
            //MediaID = int.Parse(tbxNextMediaID.Text);

            TileFilePath = tbxMediaPath.Text + "\\Tiles\\" + MediaFileName + "_stitch_files";
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
            Description = CommonName + ": " + tbxDescription1.Text;
            IsSpecimenShowcaseMedia = false;
            //MediaID = int.Parse(tbxNextMediaID.Text);

            TileFilePath = tbxMediaPath.Text + "\\Tiles\\" + MediaFileName + "_stitch_files";
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
            Description = CommonName + ": " + tbxDescription2.Text;
            IsSpecimenShowcaseMedia = false;
            //MediaID = int.Parse(tbxNextMediaID.Text);

            TileFilePath = tbxMediaPath.Text + "\\Tiles\\" + MediaFileName + "_stitch_files";
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
            Description = CommonName + ": " + tbxDescription3.Text;
            IsSpecimenShowcaseMedia = false;
            //MediaID = int.Parse(tbxNextMediaID.Text);

            TileFilePath = tbxMediaPath.Text + "\\Tiles\\" + MediaFileName + "_stitch_files";
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
            Description = CommonName + ": " + tbxDescription4.Text;
            IsSpecimenShowcaseMedia = false;
            //MediaID = int.Parse(tbxNextMediaID.Text);

            TileFilePath = tbxMediaPath.Text + "\\Tiles\\" + MediaFileName + "_stitch_files";
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
            Description = CommonName + ": " + tbxDescription5.Text;
            IsSpecimenShowcaseMedia = false;
            //MediaID = int.Parse(tbxNextMediaID.Text);

            TileFilePath = tbxMediaPath.Text + "\\Tiles\\" + MediaFileName + "_stitch_files";
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
                CollectionID, IsOnExhibit, int.Parse(RediscovRecordID), CommonName);
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

        //----------------------------------------Checks------------------------------------------------------
        private bool CheckInput()
        {
            if (tbxAccessionNum.Text == "")
            {
                rtbInformation.Text = "Please enter information for Accession Number.";
                return false;
            }
            if (!checkCategory()) return false;
            if (!checkSubCategory()) return false;
            if (!checkCirca()) return false;
            if (tbxRediscovRecordID.Text == "")
            {
                rtbInformation.Text = "Please enter information for RediscovRecordID.";
                return false;
            }
            if (cbxBase.Checked == true)
            {
                if (!checkBaseFile()) return false;
                if (tbxCommonName.Text == "")
                {
                    rtbInformation.Text = "Please enter information for Common Name.";
                    return false;
                }
                if (tbxDescription.Text == "")
                {
                    rtbInformation.Text = "Please enter information for Base Desciption.";
                    return false;
                }
            }
            if (cbxOne.Checked == true)
            {
                if (!checkOneFile()) return false;
                if (tbxDescription1.Text == "")
                {
                    rtbInformation.Text = "Please enter information for Desciption one.";
                    return false;
                }
            }
            if (cbxTwo.Checked == true)
            {
                if (!checkTwoFile()) return false;
                if (tbxDescription1.Text == "")
                {
                    rtbInformation.Text = "Please enter information for Desciption Two.";
                    return false;
                }
            }
            if (cbxThree.Checked == true)
            {
                if (!checkThreeFile()) return false;
                if (tbxDescription1.Text == "")
                {
                    rtbInformation.Text = "Please enter information for Desciption Three.";
                    return false;
                }
            }
            if (cbxFour.Checked == true)
            {
                if (!checkFourFile()) return false;
                if (tbxDescription1.Text == "")
                {
                    rtbInformation.Text = "Please enter information for Desciption Four.";
                    return false;
                }
            }
            if (cbxFive.Checked == true)
            {
                if (!checkFiveFile()) return false;
                if (tbxDescription1.Text == "")
                {
                    rtbInformation.Text = "Please enter information for Desciption Five.";
                    return false;
                }
            }
            return true;
        }

        private bool CheckInputWithSpecimenID()
        {
            if (tbxSpecimenNumber.Text == "")
            {
                rtbInformation.Text = "Please enter information for Specimen Number.";
                return false;
            }
            if (cbxBase.Checked == true)
            {
                if (!checkBaseFile()) return false;
                if (tbxCommonName.Text == "")
                {
                    rtbInformation.Text = "Please enter information for Common Name.";
                    return false;
                }
                if (tbxDescription.Text == "")
                {
                    rtbInformation.Text = "Please enter information for Base Desciption.";
                    return false;
                }
            }
            if (cbxOne.Checked == true)
            {
                if (!checkOneFile()) return false;
                if (tbxDescription1.Text == "")
                {
                    rtbInformation.Text = "Please enter information for Desciption one.";
                    return false;
                }
            }
            if (cbxTwo.Checked == true)
            {
                if (!checkTwoFile()) return false;
                if (tbxDescription1.Text == "")
                {
                    rtbInformation.Text = "Please enter information for Desciption Two.";
                    return false;
                }
            }
            if (cbxThree.Checked == true)
            {
                if (!checkThreeFile()) return false;
                if (tbxDescription1.Text == "")
                {
                    rtbInformation.Text = "Please enter information for Desciption Three.";
                    return false;
                }
            }
            if (cbxFour.Checked == true)
            {
                if (!checkFourFile()) return false;
                if (tbxDescription1.Text == "")
                {
                    rtbInformation.Text = "Please enter information for Desciption Four.";
                    return false;
                }
            }
            if (cbxFive.Checked == true)
            {
                if (!checkFiveFile()) return false;
                if (tbxDescription1.Text == "")
                {
                    rtbInformation.Text = "Please enter information for Desciption Five.";
                    return false;
                }
            }
            return true;
        }

        private bool checkBaseFile()
        {
            string file = tbxMediaPath.Text + "//" + tbxMediaName.Text + ".jpg";
            if(!File.Exists(file))
            {
                rtbInformation.Text = "Base image file not found.";
                return false;
            }
            file = tbxMediaPath.Text + "//Thumbnail//" + tbxMediaName.Text + ".jpg";
            if (!File.Exists(file))
            {
                rtbInformation.Text = "Base image Thumbnail file not found.";
                return false;
            }
            file = tbxMediaPath.Text + "\\Tiles\\" + tbxMediaName.Text + "_stitch_files\\0\\0_0.jpg";
            if (!File.Exists(file))
            {
                rtbInformation.Text = "Base image Tiles file not found.";
                return false;
            }
            else
                return true;
        }

        private bool checkOneFile()
        {
            string file = tbxMediaPath.Text + "//" + tbxMediaName1.Text + ".jpg";
            if (!File.Exists(file))
            {
                rtbInformation.Text = "Image One file not found.";
                return false;
            }
            file = tbxMediaPath.Text + "//Thumbnail//" + tbxMediaName1.Text + ".jpg";
            if (!File.Exists(file))
            {
                rtbInformation.Text = "Image One Thumbnail file not found.";
                return false;
            }
            file = tbxMediaPath.Text + "\\Tiles\\" + tbxMediaName1.Text + "_stitch_files\\0\\0_0.jpg";
            if (!File.Exists(file))
            {
                rtbInformation.Text = "Image One Tiles file not found.";
                return false;
            }
            else
                return true;
        }

        private bool checkTwoFile()
        {
            string file = tbxMediaPath.Text + "//" + tbxMediaName2.Text + ".jpg";
            if (!File.Exists(file))
            {
                rtbInformation.Text = "Image Two file not found.";
                return false;
            }
            file = tbxMediaPath.Text + "//Thumbnail//" + tbxMediaName2.Text + ".jpg";
            if (!File.Exists(file))
            {
                rtbInformation.Text = "Image Two Thumbnail file not found.";
                return false;
            }
            file = tbxMediaPath.Text + "\\Tiles\\" + tbxMediaName2.Text + "_stitch_files\\0\\0_0.jpg";
            if (!File.Exists(file))
            {
                rtbInformation.Text = "Image Two Tiles file not found.";
                return false;
            }
            else
                return true;
        }

        private bool checkThreeFile()
        {
            string file = tbxMediaPath.Text + "//" + tbxMediaName3.Text + ".jpg";
            if (!File.Exists(file))
            {
                rtbInformation.Text = "Image Three file not found.";
                return false;
            }
            file = tbxMediaPath.Text + "//Thumbnail//" + tbxMediaName3.Text + ".jpg";
            if (!File.Exists(file))
            {
                rtbInformation.Text = "Image Three Thumbnail file not found.";
                return false;
            }
            file = tbxMediaPath.Text + "\\Tiles\\" + tbxMediaName3.Text + "_stitch_files\\0\\0_0.jpg";
            if (!File.Exists(file))
            {
                rtbInformation.Text = "Image Three Tiles file not found.";
                return false;
            }
            else
                return true;
        }

        private bool checkFourFile()
        {
            string file = tbxMediaPath.Text + "//" + tbxMediaName4.Text + ".jpg";
            if (!File.Exists(file))
            {
                rtbInformation.Text = "Image Four file not found.";
                return false;
            }
            file = tbxMediaPath.Text + "//Thumbnail//" + tbxMediaName4.Text + ".jpg";
            if (!File.Exists(file))
            {
                rtbInformation.Text = "Image Four Thumbnail file not found.";
                return false;
            }
            file = tbxMediaPath.Text + "\\Tiles\\" + tbxMediaName4.Text + "_stitch_files\\0\\0_0.jpg";
            if (!File.Exists(file))
            {
                rtbInformation.Text = "Image Four Tiles file not found.";
                return false;
            }
            else
                return true;
        }

        private bool checkFiveFile()
        {
            string file = tbxMediaPath.Text + "//" + tbxMediaName5.Text + ".jpg";
            if (!File.Exists(file))
            {
                rtbInformation.Text = "Image Five file not found.";
                return false;
            }
            file = tbxMediaPath.Text + "//Thumbnail//" + tbxMediaName5.Text + ".jpg";
            if (!File.Exists(file))
            {
                rtbInformation.Text = "Image Five Thumbnail file not found.";
                return false;
            }
            file = tbxMediaPath.Text + "\\Tiles\\" + tbxMediaName5.Text + "_stitch_files\\0\\0_0.jpg";
            if (!File.Exists(file))
            {
                rtbInformation.Text = "Image Five Tiles file not found.";
                return false;
            }
            else
                return true;
        }
        

        private bool checkCategory()
        {
            if (tbxCategory.Text == "Women's Garments") return true;
            else if (tbxCategory.Text == "WG") return true;
            else if (tbxCategory.Text == "Men's Garments") return true;
            else if (tbxCategory.Text == "MG") return true;
            else if (tbxCategory.Text == "Children's Garments") return true;
            else if (tbxCategory.Text == "CG") return true;
            else if (tbxCategory.Text == "Military Uniforms") return true;
            else if (tbxCategory.Text == "MU") return true;
            else if (tbxCategory.Text == "Men's Footwear") return true;
            else if (tbxCategory.Text == "MF") return true;
            else if (tbxCategory.Text == "Women's Footwear") return true;
            else if (tbxCategory.Text == "WF") return true;
            else if (tbxCategory.Text == "Personal Gear") return true;
            else if (tbxCategory.Text == "PG") return true;
            else if (tbxCategory.Text == "Wedding Dress") return true;
            else if (tbxCategory.Text == "WD") return true;
            else if (tbxCategory.Text == "Formalwear") return true;
            else if (tbxCategory.Text == "FW") return true;
            else if (tbxCategory.Text == "Hats") return true;
            else if (tbxCategory.Text == "H") return true;
            else if (tbxCategory.Text == "Military Uniform/Hat") return true;
            else if (tbxCategory.Text == "MU/H") return true;
            else if (tbxCategory.Text == "Purses") return true;
            else if (tbxCategory.Text == "P") return true;
            else if (tbxCategory.Text == "Daywear") return true;
            else if (tbxCategory.Text == "DW") return true;
            else if (tbxCategory.Text.ToLower() == "null") return true;
            else
            {
                rtbInformation.Text = "Information entered in Category was not valid.";
                return false;
            }
        }

        private bool checkSubCategory()
        {
            if (tbxSubCategory.Text == "Women's Garments") return true;
            else if (tbxSubCategory.Text == "WG") return true;
            else if (tbxSubCategory.Text == "Men's Garments") return true;
            else if (tbxSubCategory.Text == "MG") return true;
            else if (tbxSubCategory.Text == "Children's Garments") return true;
            else if (tbxSubCategory.Text == "CG") return true;
            else if (tbxSubCategory.Text == "Military Uniforms") return true;
            else if (tbxSubCategory.Text == "MU") return true;
            else if (tbxSubCategory.Text == "Men's Footwear") return true;
            else if (tbxSubCategory.Text == "MF") return true;
            else if (tbxSubCategory.Text == "Women's Footwear") return true;
            else if (tbxSubCategory.Text == "WF") return true;
            else if (tbxSubCategory.Text == "Personal Gear") return true;
            else if (tbxSubCategory.Text == "PG") return true;
            else if (tbxSubCategory.Text == "Wedding Dress") return true;
            else if (tbxSubCategory.Text == "WD") return true;
            else if (tbxSubCategory.Text == "Formalwear") return true;
            else if (tbxSubCategory.Text == "FW") return true;
            else if (tbxSubCategory.Text == "Hats") return true;
            else if (tbxSubCategory.Text == "H") return true;
            else if (tbxSubCategory.Text == "Military Uniform/Hat") return true;
            else if (tbxSubCategory.Text == "MU/H") return true;
            else if (tbxSubCategory.Text == "Purses") return true;
            else if (tbxSubCategory.Text == "P") return true;
            else if (tbxSubCategory.Text == "Daywear") return true;
            else if (tbxSubCategory.Text == "DW") return true;
            else if (tbxSubCategory.Text.ToLower() == "null") return true;
            else if (tbxSubCategory.Text == "") return true;
            else
            {
                rtbInformation.Text = "Information entered in SubCategory was not valid.";
                return false;
            }
        }

        private bool checkCirca()
        {
            string number = tbxCirca.Text;
            if (number == "1870") return true;
            else if (number == "1910") return true;
            else if (number == "1880") return true;
            else if (number == "1940") return true;
            else if (number == "1920") return true;
            else if (number == "1890") return true;
            else if (number == "1900") return true;
            else if (number == "1980") return true;
            else if (number == "1960") return true;
            else if (number == "1970") return true;
            else if (number == "1950") return true;
            else if (number == "1830") return true;
            else if (number == "Unknown") return true;
            else if (number == "1930") return true;
            else if (number == "1850") return true;
            else if (number == "1860") return true;
            else if (number == "1990") return true;
            else if (number == "1840") return true;
            else if (number == "Currentlyon Exhibit") return true;
            else if (number == "2000") return true;
            else if (number == "2010") return true;
            else if (number == "1820") return true;
            else if (number == "1810") return true;
            else if (number == "1800") return true;
            else if (number == "Pre-1800") return true;
            else
            {
                rtbInformation.Text = "Information entered in Circa was not valid.";
                return false;
            }
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

        private void setCollection(string number)
        {
            if (number == "1870")
            { CollectionID = 1; }
            else if (number == "1910")
            { CollectionID = 2; }
            else if (number == "1880")
            { CollectionID = 3; }
            else if (number == "1940")
            { CollectionID = 4; }
            else if (number == "1920")
            { CollectionID = 5; }
            else if (number == "1890")
            { CollectionID = 7; }
            else if (number == "1900")
            { CollectionID = 8; }
            else if (number == "1980")
            { CollectionID = 9; }
            else if (number == "1960")
            { CollectionID = 10; }
            else if (number == "1970")
            { CollectionID = 11; }
            else if (number == "1950")
            { CollectionID = 12; }
            else if (number == "1830")
            { CollectionID = 13; }
            else if (number == "Unknown")
            { CollectionID = 14; }
            else if (number == "1930")
            { CollectionID = 15; }
            else if (number == "1850")
            { CollectionID = 16; }
            else if (number == "1860")
            { CollectionID = 17; }
            else if (number == "1990")
            { CollectionID = 18; }
            else if (number == "1840")
            { CollectionID = 19; }
            else if (number == "Currentlyon Exhibit")
            { CollectionID = 20; }
            else if (number == "2000")
            { CollectionID = 25; }
            else if (number == "2010")
            { CollectionID = 26; }
            else if (number == "1820")
            { CollectionID = 24; }
            else if (number == "1810")
            { CollectionID = 27; }
            else if (number == "1800")
            { CollectionID = 28; }
            else if (number == "Pre-1800")
            { CollectionID = 28; }
            else
            { number = "ERROR"; }
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
