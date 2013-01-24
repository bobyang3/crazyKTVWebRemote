﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using System.IO;
using System.Data;
using System.Xml;
using System.Collections.Specialized;
using System.Text.RegularExpressions;

namespace web
{
    public partial class find : System.Web.UI.UserControl
    {
        //DataSet1 dset = new DataSet1();

        protected void Page_Load(object sender, EventArgs e)
        {

        }


        protected void bSearch_Click(object sender, EventArgs e)
        {
            
            //clean up data on display
            GridView1.DataSource = null;
            GridView1.DataBind();

            int rowsPerPage = 100; // will be super slow if more than 2000
            //int currentPageNumber = 0; //if rowPerPage is more than 1300, then this must be NULL or nothing will be returned, if condition try to search more than one word, eg 天天, then this value must be NULL

            songList(0, rowsPerPage);

            songDGpage.Value = "0";
        }

        private void songList(int page, int rows)
        {
            //preset
            Panel2.Visible = true;
            Panel3.Visible = false;


            int currentPageNumber  = page;
            int rowsPerPage = rows;
            try
            {
                string jsonText = "";

                if (ddSearchType.SelectedValue.ToString().Trim().ToLower() == "Song".ToLower())
                {
                    jsonText = CrazyKTVWCF.QuerySong(null, null, null, "Song_SongName like '%" + tSearch.Text.ToString().Trim() + "%'", currentPageNumber, rowsPerPage, "Song_SongName"); //more than 2000 per rows will be super slow
                }
                else if (ddSearchType.SelectedValue.ToString().Trim().ToLower() == "Singer".ToLower())
                {
                    jsonText = CrazyKTVWCF.QuerySong(null, null, null, "Song_Singer like '%" + tSearch.Text.ToString().Trim() + "%'", currentPageNumber, rowsPerPage, "Song_Singer"); //more than 2000 per rows will be super slow
                }
                else if (ddSearchType.SelectedValue.ToString().Trim().ToLower() == "NewSongs".ToLower())
                {
                    jsonText = CrazyKTVWCF.QuerySong(null, null, null, "Song_CreatDate >= '" + DateTime.Now.AddDays(-90).ToString("yyyy/MM/dd") + "'", currentPageNumber, rowsPerPage, "Song_CreatDate desc, Song_SongName"); //more than 2000 per rows will be super slow
                }
                else if (ddSearchType.SelectedValue.ToString().Trim().ToLower() == "male".ToLower())
                {
                    jsonText = CrazyKTVWCF.QuerySong(null, null, null, "Song_SingerType=0", currentPageNumber, rowsPerPage, "Song_Singer, Song_CreatDate desc, Song_SongName"); //more than 2000 per rows will be super slow
                    tSearch.Text = "";
                }
                else if (ddSearchType.SelectedValue.ToString().Trim().ToLower() == "female".ToLower())
                {
                    jsonText = CrazyKTVWCF.QuerySong(null, null, null, "Song_SingerType=1", currentPageNumber, rowsPerPage, "Song_Singer, Song_CreatDate desc, Song_SongName"); //more than 2000 per rows will be super slow
                    tSearch.Text = "";
                }
                else if (ddSearchType.SelectedValue.ToString().Trim().ToLower() == "chorus".ToLower())
                {
                    jsonText = CrazyKTVWCF.QuerySong(null, null, null, "Song_SingerType=3", currentPageNumber, rowsPerPage, "Song_SongName, Song_Singer, Song_CreatDate desc"); //more than 2000 per rows will be super slow
                    tSearch.Text = "";
                }
                else if (ddSearchType.SelectedValue.ToString().Trim().ToLower() == "wordcount".ToLower())
                {
                    jsonText = CrazyKTVWCF.QuerySong(null, null, null, "Song_WordCount=" + tSearch.Text.ToString().Trim(), currentPageNumber, rowsPerPage, "Song_CreatDate desc"); //more than 2000 per rows will be super slow
                }
                else if (ddSearchType.SelectedValue.ToString().Trim().ToLower() == "toporder".ToLower())
                {
                    jsonText = CrazyKTVWCF.QuerySong(null, null, null, "Song_PlayCount >= 1 ", currentPageNumber, rowsPerPage, "Song_PlayCount desc, Song_CreatDate desc"); //more than 2000 per rows will be super slow
                    tSearch.Text = "";
                }
                else if (ddSearchType.SelectedValue.ToString().Trim().ToLower() == "Favorites".ToLower())
                {
                    tSearch.Text = ""; 
                    Panel2.Visible = false;
                    Panel3.Visible = true;

                    jsonText = CrazyKTVWCF.FavoriteUser(0, 200);

                    DataTable dt2 = GlobalFunctions.JsontoDataTable(jsonText);

                    DataView dv2 = new DataView(dt2);
                    //dv.Sort = "Song_Singer asc, Song_SongName asc, Song_Id asc";

                    GridView2.DataSource = dv2;
                    GridView2.DataBind();

                }

                //jsonText = jsonText.TrimStart('"');
                //jsonText = jsonText.TrimEnd('"');
                //jsonText = Regex.Replace(jsonText, @"\\""", @"""");

                try
                {
                    DataTable dt = GlobalFunctions.JsontoDataTable(jsonText);

                    DataView dv = new DataView(dt);
                    //dv.Sort = "Song_Singer asc, Song_SongName asc, Song_Id asc";

                    GridView1.DataSource = dv;
                    GridView1.DataBind();

                    if (dv.Count == rows)
                    {
                        BNext.Visible = true;
                        if (page > 0)
                        {
                            BPrevious.Visible = true;
                        }
                        else
                        {
                            BPrevious.Visible = false;
                        }

                    }
                    else
                    {
                        BNext.Visible = false;
                        if (page > 0)
                        {
                            BPrevious.Visible = true;
                        }
                        else
                        {
                            BPrevious.Visible = false;
                        }
                    }
                }
                catch { }

               
                


            }
            catch { }

            //  GlobalFunctions.DerializetoDataTable(); //test data
           

        }



        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            var data = GridView1.DataKeys[Convert.ToInt32(e.CommandArgument)].Values[0]; //get hiddent Song_ID

            if (e.CommandName.ToLower().Trim() == "Add".ToLower().Trim())
            {
                // Convert the row index stored in the CommandArgument
                // property to an Integer.
                int index = Convert.ToInt32(e.CommandArgument);

                // Get the last name of the selected author from the appropriate
                // cell in the GridView control.

                //GridViewRow selectedRow = GridView1.Rows[index];
                //TableCell Song_Id = selectedRow.Cells[1];
                //CrazyKTVWCF.wcf_addsong(Song_Id.Text.Trim());

                CrazyKTVWCF.wcf_addsong(data.ToString().Trim());
            } else if (e.CommandName.ToLower().Trim() == "Insert".ToLower().Trim())
            {
                // Convert the row index stored in the CommandArgument
                // property to an Integer.
                int index = Convert.ToInt32(e.CommandArgument);

                // Get the last name of the selected author from the appropriate
                // cell in the GridView control.
                //GridViewRow selectedRow = GridView1.Rows[index];
                //TableCell Song_Id = selectedRow.Cells[1];
                //CrazyKTVWCF.wcf_insertsong(Song_Id.Text.Trim());

                CrazyKTVWCF.wcf_addsong(data.ToString().Trim());
            }




        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;            
            GridView1.DataBind();
        }

        protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
        {
            //if (GridViewSortDirection == null)
            //{
            //    e.SortDirection = SortDirection.Descending;
            //}
            //else if (GridViewSortDirection == SortDirection.Ascending)
            //{
            //    e.SortDirection = SortDirection.Descending;
            //}
            //else if (GridViewSortDirection == SortDirection.Descending)
            //{
            //    e.SortDirection = SortDirection.Ascending;
            //}

            //GridViewSortDirection = e.SortDirection;
        }

        protected void BNext_Click(object sender, EventArgs e)
        {
            //clean up data on display
            GridView1.DataSource = null;
            GridView1.DataBind();

            songList(1 + int.Parse(songDGpage.Value.ToString()), 100);
            songDGpage.Value=(1 + int.Parse(songDGpage.Value.ToString())).ToString();
        }

        protected void BPrevious_Click(object sender, EventArgs e)
        {
            //clean up data on display
            GridView1.DataSource = null;
            GridView1.DataBind();

            songList(int.Parse(songDGpage.Value.ToString())-1, 100);
            songDGpage.Value=(int.Parse(songDGpage.Value.ToString()) -1).ToString();
        }

        protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //clean up data on display
            GridView1.DataSource = null;
            GridView1.DataBind();
            tSearch.Text = "";
            Panel2.Visible = true;
            Panel3.Visible = false;
            BNext.Visible = false;
            BPrevious.Visible = false;

            var data = GridView2.DataKeys[Convert.ToInt32(e.CommandArgument)].Values[0]; //get DataKeyNames="User_Name"

            string jsonText = CrazyKTVWCF.FavoriteSong(data.ToString().Trim(), 0, 100);

            DataTable dt3 = GlobalFunctions.JsontoDataTable(jsonText);
            DataView dv3 = new DataView(dt3);
            //dv.Sort = "Song_Singer asc, Song_SongName asc, Song_Id asc";

            GridView1.DataSource = dv3;
            GridView1.DataBind();

            int page = 0;

            if (dv3.Count == 100)
            {
                BNext.Visible = true;
                if (page > 0)
                {
                    BPrevious.Visible = true;
                }
                else
                {
                    BPrevious.Visible = false;
                }

            }
            else
            {
                BNext.Visible = false;
                if (page > 0)
                {
                    BPrevious.Visible = true;
                }
                else
                {
                    BPrevious.Visible = false;
                }
            }



        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        //public SortDirection GridViewSortDirection
        //{
        //    //get
        //    //{
        //    //    if (ViewState["sortDirection"] == null)
        //    //        ViewState["sortDirection"] = SortDirection.Ascending;
        //    //    return (SortDirection)ViewState["sortDirection"];
        //    //}
        //    //set
        //    //{
        //    //    ViewState["sortDirection"] = value;
        //    //}
        //}


    }
}