//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Linq;
//using System.Text;
//using System.Windows;

//namespace NuttinButCDs
//{
//    class Genres : List<string>
//    {
//        private NuttinButCDsDBDataSet CDsDataSet = new NuttinButCDsDBDataSet();
//        private DataTable genreDataTable = new DataTable("myGenres");
//        private NuttinButCDsDBDataSetTableAdapters.GenresTableAdapter genresTableAdapter =
//            new NuttinButCDsDBDataSetTableAdapters.GenresTableAdapter();

//        // TODO: make this a centrally located tool rather than replicate the code!
//        public static T ConvertFromDBVal<T>(object obj)
//        {
//            if (obj == null || obj == DBNull.Value)
//            {
//                return default(T);
//            }
//            else
//            {
//                return (T)obj;
//            }
//        }

//        public Genres()
//        {
//            genresTableAdapter.Fill(CDsDataSet.Genres);
//            genreDataTable = genresTableAdapter.GetData();

//            DataRowCollection genreRows = genreDataTable.Rows;
//            foreach (DataRow aRow in genreRows)
//            {
//                base.Add(ConvertFromDBVal<string>(aRow["GenreName"]));
//            }
//        }

//        // TODO: Implement remove
//        public new bool Remove(string genre)
//        {
//            bool result = false;

//            if (genre == null)
//            {
//                MessageBox.Show("Remove failed: null genre");
//                return result;
//            }

//            return result;
//        }

//        public new void Add(string genre)
//        {
//            NuttinButCDsDBDataSet.GenresRow newRow;

//            if (genre == null)
//            {
//                MessageBox.Show("Add failed: null genre");
//                return;
//            }

//            newRow = CDsDataSet.Genres.NewGenresRow();

//            newRow.BeginEdit();
//            newRow["GenreName"] = genre;
//            newRow.EndEdit();

//            CDsDataSet.Genres.Rows.Add(newRow);

//            try
//            {
//                // TODO: Validate data first!
//                genresTableAdapter.Update(CDsDataSet.Genres);
//                CDsDataSet.Genres.AcceptChanges();
//                genresTableAdapter.Fill(CDsDataSet.Genres);
//                genreDataTable = genresTableAdapter.GetData();
//                //MessageBox.Show("Update successful");
//            }
//            catch (System.Exception ex)
//            {
//                MessageBox.Show("Update genre failed: " + ex.Message);
//            }
//        }
//    }
//}
