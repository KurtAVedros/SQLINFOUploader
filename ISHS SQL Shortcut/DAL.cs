using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISHS_SQL_Shortcut
{
    class DAL
    {
        // for local
        private static string ReadOnlyConnectionString = "server=localhost;Database=ISHS_Dev;User ID=iusr_ishs_reader;Password=9f#l1N4MDuE)";
        private static string EditOnlyConnectionString = "server=localhost;Database=ISHS_Dev;User ID=iusr_ishs_editor;Password=$s!kFB_e#ad6V";


        // for textile database
        //private static string ReadOnlyConnectionString = "server=164.165.207.102;Database=ISHS_Dev;User ID=iusr_ishs_reader;Password=9f#l1N4MDuE)";
        //private static string EditOnlyConnectionString = "server=164.165.207.102;Database=ISHS_Dev;User ID=iusr_ishs_editor;Password=$s!kFB_e#ad6V";

        private DAL()
        {
        }

        public static void setDatabase(int num)
        {
            if(num == 1)
            {
                ReadOnlyConnectionString = "server=164.165.207.102;Database=ishs_textile;User ID=ISHSISUdbo;Password=The Museum Encountered Bengals!";
                EditOnlyConnectionString = "server=164.165.207.102;Database=ishs_textile;User ID=ISHSISUdbo;Password=The Museum Encountered Bengals!";
            }
            else
            {
                ReadOnlyConnectionString = "server=localhost;Database=ISHS_Dev;User ID=iusr_ishs_reader;Password=9f#l1N4MDuE)";
                EditOnlyConnectionString = "server=localhost;Database=ISHS_Dev;User ID=iusr_ishs_editor;Password=$s!kFB_e#ad6V";
            }
        }

        #region Media

        public static int MediaAdd(int SpecimenID, string FileName, string Extension, 
            string MimeType, int MediaDataID, int ThumbnailDataID, int Height, int Width, string AltText, bool IsSpecimenShowcaseMedia)
        {
            int retInt = -1;

            SqlCommand comm = new SqlCommand();
            try
            {
                comm.CommandText = "sproc_MediaAdd";
                comm.Parameters.AddWithValue("@SpecimenID", SpecimenID);
                comm.Parameters.AddWithValue("@FileName", FileName);
                comm.Parameters.AddWithValue("@Extension", Extension);
                comm.Parameters.AddWithValue("@MimeType", MimeType);
                comm.Parameters.AddWithValue("@MediaDataID", MediaDataID);
                comm.Parameters.AddWithValue("@ThumbnailDataID", ThumbnailDataID);
                comm.Parameters.AddWithValue("@Height", Height);
                comm.Parameters.AddWithValue("@Width", Width);
                comm.Parameters.AddWithValue("@AltText", AltText);
                comm.Parameters.AddWithValue("@IsSpecimenShowcaseMedia", IsSpecimenShowcaseMedia);

                SqlParameter retParameter;
                retParameter = new SqlParameter("@MediaID", System.Data.SqlDbType.Int);
                retParameter.Direction = System.Data.ParameterDirection.Output;
                comm.Parameters.Add(retParameter);

                comm.Connection = new SqlConnection(EditOnlyConnectionString);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Connection.Open();

                int rowsAffected = comm.ExecuteNonQuery();
                if (rowsAffected != 1)
                {
                    //There was a problem
                    retInt = -1;
                }
                else
                    retInt = (int)retParameter.Value;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }
            return retInt;
        }
        #endregion

        #region MediaDate

        public static int MediaDataAdd(int MediaDataID, byte[] Data)
        {
            int retInt = -1;

            SqlCommand comm = new SqlCommand();
            try
            {
                comm.CommandText = "sproc_MediaDataAdd";
                comm.Parameters.AddWithValue("@Data", Data);

                SqlParameter retParameter;
                retParameter = new SqlParameter("@MediaDateID", System.Data.SqlDbType.Int);
                retParameter.Direction = System.Data.ParameterDirection.Output;
                comm.Parameters.Add(retParameter);

                comm.Connection = new SqlConnection(EditOnlyConnectionString);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Connection.Open();

                int rowsAffected = comm.ExecuteNonQuery();
                if (rowsAffected != 1)
                {
                    //There was a problem
                    retInt = -1;
                }
                else
                    retInt = (int)retParameter.Value;
                MediaDataID = retInt;
            }
            catch (Exception ex)
            {
                retInt = -1;
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }
            return retInt;

        }
        #endregion

        #region Tile

        public static void TileAdd(int SpecimenID, int MediaID, int Level, int Column, int Row, byte[] Image)
        {
            SqlCommand comm = new SqlCommand();
            try
            {
                comm.CommandText = "sproc_TileAdd";
                comm.Parameters.AddWithValue("@SpecimenID", SpecimenID);
                comm.Parameters.AddWithValue("@MediaID", MediaID);
                comm.Parameters.AddWithValue("@Level", Level);
                comm.Parameters.AddWithValue("@Column", Column);
                comm.Parameters.AddWithValue("@Row", Row);
                comm.Parameters.AddWithValue("@Image", Image);

                comm.Connection = new SqlConnection(EditOnlyConnectionString);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Connection.Open();

                int rowsAffected = comm.ExecuteNonQuery();
                if (rowsAffected != 1)
                {
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }

        }
        #endregion

        #region Specimen

        public static int SpecimenAdd(int SpecimenID, string AccessionNumber, int CategoryID, int SubCategoryID,
            string Circa, int CollectionID, bool IsOnExhibit, int RediscovryID, string CommonName)
        {
            int retInt = -1;

            SqlCommand comm = new SqlCommand();
            try
            {
                comm.CommandText = "sproc_SpecimenAdd";
                comm.Parameters.AddWithValue("@AccessionNumber", AccessionNumber);
                comm.Parameters.AddWithValue("@CategoryID", CategoryID);
                if(SubCategoryID == 0)
                    comm.Parameters.AddWithValue("@SubCategoryID", DBNull.Value);
                else
                    comm.Parameters.AddWithValue("@SubCategoryID", SubCategoryID);
                comm.Parameters.AddWithValue("@Circa", Circa);
                comm.Parameters.AddWithValue("@MaterialID", DBNull.Value);
                comm.Parameters.AddWithValue("@SecondaryMaterialID", DBNull.Value);
                comm.Parameters.AddWithValue("@CollectionID", CollectionID);
                if(RediscovryID == 0)
                    comm.Parameters.AddWithValue("@RediscovRecordID", DBNull.Value);
                else
                    comm.Parameters.AddWithValue("@RediscovRecordID", RediscovryID);
                comm.Parameters.AddWithValue("@IsOnExhibit", IsOnExhibit);
                comm.Parameters.AddWithValue("@CommonName", CommonName);

                SqlParameter retParameter;
                retParameter = new SqlParameter("@SpecimenID", System.Data.SqlDbType.Int);
                retParameter.Direction = System.Data.ParameterDirection.Output;
                comm.Parameters.Add(retParameter);

                comm.Connection = new SqlConnection(EditOnlyConnectionString);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Connection.Open();

                int rowsAffected = comm.ExecuteNonQuery();
                if (rowsAffected != 1)
                {
                    //There was a problem
                    retInt = -1;
                }
                else
                    retInt = (int)retParameter.Value;
                SpecimenID = retInt;
            }
            catch (Exception ex)
            {
                retInt = -1;
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }
            return retInt;
        }
        #endregion
    }
}
