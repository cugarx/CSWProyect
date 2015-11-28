using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LibraryCSW.infrastructure;
using System.Threading.Tasks;

public partial class _Default : Page
{
    protected async void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            serviceDAO service = new serviceDAO();
            try
            {

                await loadAuthors(service);
                await loadAuthorsActives(service);
                await loadBooks(service, 0, 0);
                await loadCategories(service);
            }
            catch (Exception Ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), Guid.NewGuid().ToString(), "alert('An error has ocurred, please contact with your software provider');", true);
            }
        }
    }

    private async Task loadBooks(serviceDAO service, int init, int currentPage)
    {
        List<BookFull> books;
        if (ddlAuthorsActives.SelectedValue=="0")
            books = await service.GetAllBooksFull(0);
        else        
            books = await service.GetAllBooksFull(Int32.Parse(ddlAuthorsActives.SelectedValue));        
        int fin = init + gvBooks.PageSize;
        if (books != null && fin > books.Count)
            fin = books.Count;
        gvBooks.PageIndex = currentPage;

        gvBooks.DataSource = books;
        gvBooks.DataBind();
        for (int i = init; i < fin; i++)
        {
            ((ImageButton)gvBooks.Rows[i].Cells[6].Controls[0]).ImageUrl = "images/flags/" + books[i].Country + ".png";
            ((ImageButton)gvBooks.Rows[i].Cells[7].Controls[0]).Attributes.Add("OnClick", "if(!confirm('The entry will be deleted, are you sure?'))return false;");
        }
    }

    private async Task loadCategories(serviceDAO service)
    {
        List<Category> category = await service.GetAllCategories();
        ddlCategory.DataSource = category;
        ddlCategory.DataValueField = "Id";
        ddlCategory.DataTextField = "Name";
        ListItem selec = new ListItem("Select...", "0");

        ddlCategory.DataBind();
        selec.Selected = true;
        ddlCategory.Items.Insert(0, selec);
    }

    private async Task loadAuthors(serviceDAO service)
    {
        List<Author> authors = await service.GetAllAuthors();
        ddlAuthor.DataSource = authors;
        ddlAuthor.DataValueField = "Id";
        ddlAuthor.DataTextField = "Name";
        ListItem selec = new ListItem("Select...", "0");

        ddlAuthor.DataBind();
        selec.Selected = true;
        ddlAuthor.Items.Insert(0, selec);
    }

    private async Task loadAuthorsActives(serviceDAO service)
    {
        List<BookFull> books = await service.GetAllBooksFull(0);


        foreach (BookFull bf in books)
        {
            ListItem item = new ListItem();
            item.Text = bf.Author;
            item.Value = bf.IdAuthor.ToString();
            if(ddlAuthorsActives.Items.FindByValue(item.Value)==null)
                ddlAuthorsActives.Items.Add(item);
        }
        ListItem selec = new ListItem("All", "0");
        ddlAuthorsActives.DataBind();


        selec.Selected = true;
        ddlAuthorsActives.Items.Insert(0, selec);
    }

    protected async void gvBooks_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "eliminar")
        {

            serviceDAO service = new serviceDAO();
            try
            {
                List<Book> book = await service.GetBook(Convert.ToInt32(gvBooks.Rows[Convert.ToInt32(e.CommandArgument)].Cells[0].Text));
                service.DeleteBook(book[0]);
                await loadBooks(service, 0, 0);
            }
            catch (Exception Ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), Guid.NewGuid().ToString(), "alert('An error has ocurred, please contact with your software provider');", true);
            }
        }
    }

    protected void btnAddBook_Click(object sender, EventArgs e)
    {
        ModalPopupExtender1.Show();
        lblError.Text = string.Empty;
    }

    protected async void btnAddBookAcept_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            try
            {
                lblError.Text = string.Empty;
                serviceDAO service = new serviceDAO();
                Book book = new Book();
                book.IdAuthor = Int32.Parse(ddlAuthor.SelectedValue);
                book.IdCategory = Int32.Parse(ddlCategory.SelectedValue);
                book.ISBN = txtISBN.Text;
                book.Title = txtTitle.Text;
                book.Publisher = txtPublisher.Text;
                service.AddBook(book);

                ddlAuthor.SelectedValue = "0";
                ddlCategory.SelectedValue = "0";
                txtISBN.Text = txtTitle.Text = txtPublisher.Text = string.Empty;
                await loadBooks(service, 0, 0);
                ModalPopupExtender1.Hide();
            }
            catch (Exception Ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), Guid.NewGuid().ToString(), "alert('An error has ocurred, please contact with your software provider');", true);
            }
        }
        else
        {
            lblError.Text = "*All information is required";
            ModalPopupExtender1.Show();
        }
    }

    protected async void ddlAuthorsActives_SelectedIndexChanged(object sender, EventArgs e)
    {
        serviceDAO service = new serviceDAO();
        List<BookFull> books;
        if (ddlAuthorsActives.SelectedValue == "0")
            books = await service.GetAllBooksFull(0);
        else
            books = await service.GetAllBooksFull(Int32.Parse(ddlAuthorsActives.SelectedValue));


        gvBooks.DataSource = books;
        gvBooks.DataBind();
        for (int i = 0; i < gvBooks.Rows.Count; i++)
        {
            ((ImageButton)gvBooks.Rows[i].Cells[6].Controls[0]).ImageUrl = "images/flags/" + books[i].Country + ".png";
            ((ImageButton)gvBooks.Rows[i].Cells[7].Controls[0]).Attributes.Add("OnClick", "if(!confirm('The entry will be deleted, are you sure?'))return false;");
        }
    }
    
}