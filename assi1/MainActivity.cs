using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace assi1
{



    [Activity(Label = "TakeNote", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {

        DBHelper sqldb;

        Button btnAdd;
        ImageButton btnSearch;

        EditText txtSearch;

        ListView listnote;

        int flag = 0;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            btnAdd = FindViewById<Button>(Resource.Id.btnAdd);
            btnSearch = FindViewById<ImageButton>(Resource.Id.btnSearch);

            txtSearch = FindViewById<EditText>(Resource.Id.txtSearch);

            listnote = FindViewById<ListView>(Resource.Id.lstNotes);

            sqldb = new DBHelper("note_db");

            btnAdd.Click+= delegate { Add(); };
            
            btnSearch.Click += delegate { EnableSearch() ; };
            
            

            //Add ItemClick event handler to ListView instance
            listnote.ItemClick += new EventHandler<AdapterView.ItemClickEventArgs>(item_Clicked);

            
            txtSearch.Visibility=ViewStates.Invisible;

            GetCursorView();

        }

        public void Add()
        {
            var AddingPage = new Intent(this, typeof(AddingPage));
            
            StartActivity(AddingPage);
            Finish();
            
        }

        public void EnableSearch()
        {
            if (flag == 0)
            {
                txtSearch.Text = "";
                txtSearch.Visibility = ViewStates.Visible;
                flag = 1;
            }
            else if(flag==1 && txtSearch.Visibility==ViewStates.Visible)
            {
                Android.Database.ICursor sqldb_cursor = sqldb.GetRecordCursor("TitleNote", txtSearch.Text.Trim());
                if (sqldb_cursor != null)
                {
                    sqldb_cursor.MoveToFirst();
                    string[] from = new string[] { "_id", "TitleNote" };
                    int[] to = new int[] {
                    Resource.Id.Idrow,
                    Resource.Id.Titlerow,

                };
                    //Creates a SimplecursorAdapter for ListView object
                    SimpleCursorAdapter sqldb_adapter = new SimpleCursorAdapter(this, Resource.Layout.ListNote, sqldb_cursor, from, to);
                    listnote.Adapter = sqldb_adapter;
                    txtSearch.Visibility = ViewStates.Invisible;
                    flag = 0;

                }
                else
                {
                    Toast toast = Toast.MakeText(this, "Error, Try again", ToastLength.Short);
                    toast.Show();
                    

                }
            }
           
            
        }

       

        //Gets the cursor view to show all records
        public void GetCursorView()
        {
            Android.Database.ICursor sqldb_cursor = sqldb.GetRecordCursor();
            if (sqldb_cursor != null)
            {
                sqldb_cursor.MoveToFirst();
                string[] from = new string[] { "_id", "TitleNote" };
                int[] to = new int[] {
                    Resource.Id.Idrow,
                    Resource.Id.Titlerow,

                };
                //Creates a SimplecursorAdapter for ListView object
                SimpleCursorAdapter sqldb_adapter = new SimpleCursorAdapter(this, Resource.Layout.ListNote, sqldb_cursor, from, to);
                listnote.Adapter = sqldb_adapter;
            }
            else
            {
                
               
            }
        }

        void item_Clicked(object sender, AdapterView.ItemClickEventArgs e)
        {
            
            //Gets TextView object instance from record_view layout
            TextView shId = e.View.FindViewById<TextView>(Resource.Id.Idrow);

            //Reads values and sets to EditText object instances

            Note temp = new Note();
            temp = GetRecordbyID(Convert.ToInt32(shId.Text));

            var EdtingPage = new Intent(this, typeof(EdtingPage));
            EdtingPage.PutExtra("IdNote", temp.getidN().ToString());
            StartActivity(EdtingPage);
            Finish();

        }

        //get Id in listview
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

