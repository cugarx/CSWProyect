using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LibraryCSW.infrastructure;
using System.Threading.Tasks;

public partial class About : Page
{
    protected async void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            serviceDAO service = new serviceDAO();
            try
            {
                await loadAuthors(service, 0, 0);
                await loadCountries(service);
            }
            catch (Exception Ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), Guid.NewGuid().ToString(), "alert('An error has ocurred, please contact with your software provider');", true);
            }
        }
    }


    private async Task loadAuthors(serviceDAO service, int init, int currentPage)
    {
        List<Author> authors = await service.GetAllAuthors();
        int fin = init + gvAuthors.PageSize;
        if (authors != null && fin > authors.Count)
            fin = authors.Count;
        gvAuthors.PageIndex = currentPage;

        gvAuthors.DataSource = authors;
        gvAuthors.DataBind();
        for (int i = init; i < fin; i++)
        {
            List<Country> countries = await service.GetCountry(authors[i].IdCountry);
            if (countries != null && countries.Count > 0)
                ((ImageButton)gvAuthors.Rows[i].Cells[3].Controls[0]).ImageUrl = "images/flags/" + countries[0].Code + ".png";
            ((ImageButton)gvAuthors.Rows[i].Cells[5].Controls[0]).Attributes.Add("OnClick", "if(!confirm('The entry will be deleted, are you sure?'))return false;");
        }
    }


    private async Task loadCountries(serviceDAO service)
    {
        List<Country> country = await service.GetAllCountry();
        ddlCountry.DataSource = country;
        ddlCountry.DataValueField = "Id";
        ddlCountry.DataTextField = "NameEn";
        ListItem selec = new ListItem("Select...", "0");

        ddlCountry.DataBind();
        selec.Selected = true;
        ddlCountry.Items.Insert(0, selec);
    }

    protected async void gvAuthors_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        serviceDAO service = new serviceDAO();
        try
        {
            List<Author> author = await service.GetAuthor(Convert.ToInt32(gvAuthors.Rows[Convert.ToInt32(e.CommandArgument)].Cells[0].Text));
            if (author != null && author.Count > 0)
            {
                if (e.CommandName == "editar")
                {
                    ModalPopupExtender1.Show();
                    List<Author> authors = await service.GetAuthor(author[0].Id);
                    lblIDAuthor.Text = authors[0].Id.ToString();
                    txtAuthorName.Text = authors[0].Name;
                    txtAuthorLastName.Text = authors[0].LastName;
                    ddlCountry.SelectedValue = authors[0].IdCountry.ToString();
                    btnAddAuthor.Text = "Update";

                }
                else if (e.CommandName == "eliminar")
                {
                    List<Book> bookAssignments = await service.GetAssignmentsAuthors(Convert.ToInt32(gvAuthors.Rows[Convert.ToInt32(e.CommandArgument)].Cells[0].Text));
                    if (bookAssignments != null && bookAssignments.Count > 1)
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), Guid.NewGuid().ToString(), "alert('Invalid operation, The item is assigned to one or more books');", true);
                    else
                    {
                        service.DeleteAuthor(author[0]);
                        await loadAuthors(service, 0, 0);
                    }
                }
            }
        }
        catch (Exception Ex)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), Guid.NewGuid().ToString(), "alert('An error has ocurred, please contact with your software provider');", true);
        }
    }

    protected async void gvAuthors_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        serviceDAO service = new serviceDAO();
        try
        {
            await loadAuthors(service, (gvAuthors.PageSize * e.NewPageIndex), e.NewPageIndex);
        }
        catch (Exception Ex)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), Guid.NewGuid().ToString(), "alert('An error has ocurred, please contact with your software provider');", true);
        }
    }

    protected void BtnNew_Click(object sender, EventArgs e)
    {
        txtAuthorName.Text = txtAuthorLastName.Text = string.Empty;
        ddlCountry.SelectedValue = "0";
        btnAddAuthor.Text = "Add";
        ModalPopupExtender1.Show();
    }

    protected async void btnAddAuthor_Click(object sender, EventArgs e)
    {
        try
        {
            serviceDAO service = new serviceDAO();
            if (Page.IsValid)
            {
                lblError.Text = string.Empty;

                if (btnAddAuthor.Text == "Add")
                {
                    Author author = new Author();
                    author.Name = txtAuthorName.Text;
                    author.LastName = txtAuthorLastName.Text;
                    author.IdCountry = Int32.Parse(ddlCountry.SelectedValue);
                    service.AddAuthor(author);
                }
                else
                {
                    List<Author> author = await service.GetAuthor(Int32.Parse(lblIDAuthor.Text));
                    author[0].Name = txtAuthorName.Text;
                    author[0].LastName = txtAuthorLastName.Text;
                    author[0].IdCountry = Int32.Parse(ddlCountry.SelectedValue);
                    service.UpdateAuthor(author[0]);

                    await loadAuthors(service, 0, 0);
                }

                await loadAuthors(service, 0, 0);
                txtAuthorName.Text = txtAuthorLastName.Text = string.Empty;
                ddlCountry.SelectedValue = "0";
                btnAddAuthor.Text = "Add";

            }
            else
            {
                lblError.Text = "*All information is required";
                ModalPopupExtender1.Show();
            }
        }
        catch (Exception Ex)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), Guid.NewGuid().ToString(), "alert('An error has ocurred, please contact with your software provider');", true);
        }
    }
}