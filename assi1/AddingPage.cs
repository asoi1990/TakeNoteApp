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

namespace assi1
{
    [Activity(Label = "AddingPage")]
    public class AddingPage : Activity
    {
        Button btnAddA;
        Button btnResetA;
        Button btnExitA;

        EditText txtTitleA;
        EditText txtContentA;

        DBHelper sqldb;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.AddingPage);

            // Create your application here

            btnAddA = FindViewById<Button>(Resource.Id.btnAddA);
            btnResetA = FindViewById<Button>(Resource.Id.btnResetA);
            btnExitA = FindViewById<Button>(Resource.Id.btnExitA);

            txtTitleA = FindViewById<EditText>(Resource.Id.txtTitleA);
            txtContentA = FindViewById<EditText>(Resource.Id.txtContentA);

            sqldb = new DBHelper("note_db");

            

            btnAddA.Click += delegate { AddingClick(); };
            btnResetA.Click += delegate { ResetingClick(); };
            btnExitA.Click += delegate { ExitClick(); };


        }

        public void ResetingClick()
        {
            txtTitleA.Text = "";
            txtContentA.Text = "";
        }

        public string GetLastID()
        {
            Android.Database.ICursor sqldb_cursor = sqldb.getLastID();

            string x = "";


            if (sqldb_cursor.Count != 0)
            {
                sqldb_cursor.MoveToFirst();

                x = sqldb_cursor.GetString(0);


            }
            else
                x = "";

            return x;
        }

        public void AddingClick()
        {
            
            string x = GetLastID();


            if (x != "") //next record after first
            {

                Note note = new Note((Convert.ToInt32(x) + 1), txtTitleA.Text.Trim(), txtContentA.Text.Trim());
                sqldb.AddRecord(note);
                Toast toast = Toast.MakeText(this, "Success", ToastLength.Short);
                toast.Show();
            }
            else //first record
            {
                Note note = new Note(1, txtTitleA.Text, txtContentA.Text);
                
                sqldb.AddRecord(note);
                Toast toast = Toast.MakeText(this, "Success", ToastLength.Short);
                toast.Show();

            }
            
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

    }
}