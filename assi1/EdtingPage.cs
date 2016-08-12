using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Graphics.Drawables;
using Android.Graphics;


//https://developer.xamarin.com/recipes/android/fundamentals/activity/pass_data_between_activity/

namespace assi1
{
    [Activity(Label = "EdtingPage")]
    public class EdtingPage : Activity
    {

        Button btnEditE;
        Button btnRemoveE;
        Button btnExitE;

        EditText txtTitleE;
        EditText txtContentE;

        DBHelper sqldb;

        string IDNote;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.EdtingPage);

            IDNote = Intent.GetStringExtra("IdNote") ?? "Data not available";

            btnEditE = FindViewById<Button>(Resource.Id.btnEditE);
            btnRemoveE = FindViewById<Button>(Resource.Id.btnDeleteE);
            btnExitE = FindViewById<Button>(Resource.Id.btnExitE);

            txtTitleE = FindViewById<EditText>(Resource.Id.txtTitleE);
            txtContentE = FindViewById<EditText>(Resource.Id.txtContentE);

            sqldb = new DBHelper("note_db");



            btnEditE.Click += delegate { UpdateRecord(); };
            btnRemoveE.Click += delegate { RemoveRecord(Convert.ToInt32( IDNote)); };
            btnExitE.Click += delegate { ExitClick(); };
            txtTitleE.Text= GetRecordbyID(Convert.ToInt32(IDNote)).gettitleN();
            txtContentE.Text = GetRecordbyID(Convert.ToInt32(IDNote)).getcontentN();

            
        }

        public void UpdateRecord()
        {
            Note UNote = new Note(Convert.ToInt32(IDNote), txtTitleE.Text, txtContentE.Text);
            sqldb.UpdateRecord(UNote);

            Toast toast = Toast.MakeText(this, "Success", ToastLength.Short);
            toast.Show();
        }

        public void RemoveRecord(int id)
        {
            var builder = new AlertDialog.Builder(this);
            builder.SetMessage("Do you want to remove "+txtTitleE.Text+" ?");
            builder.SetPositiveButton("Yes", (s, e) =>
            {
                sqldb.DeleteRecord(id);

                Toast toast = Toast.MakeText(this, "Success", ToastLength.Short);
                toast.Show();

                var MainPage = new Intent(this, typeof(MainActivity));

                StartActivity(MainPage);
                Finish();
            });
            builder.SetNegativeButton("Cancel", (s, e) => { });
            builder.Create().Show();


            

        }

        public void ExitClick()
        {
            var builder = new AlertDialog.Builder(this);
            builder.SetMessage("Do you want to exit?");
            builder.SetPositiveButton("Yes", (s, e) =>
            {
                var MainPage = new Intent(this, typeof(MainActivity));

                StartActivity(MainPage);
                Finish();
            });
            builder.SetNegativeButton("Cancel", (s, e) => { });
            builder.Create().Show();

           
        }

        public Note GetRecordbyID(int id)
        {
            Android.Database.ICursor sqldb_cursor = sqldb.GetRecordCursorByID(id);

            Note x = new Note();


            if (sqldb_cursor != null)
            {
                sqldb_cursor.MoveToFirst();

                x.setidN(Convert.ToInt32(sqldb_cursor.GetString(0)));
                x.settitleN(sqldb_cursor.GetString(1));
                x.setcontentN(sqldb_cursor.GetString(2));


            }
            else
            {

            }


            return x;
        }

      

    }
}