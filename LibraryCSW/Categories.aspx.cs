using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LibraryCSW.infrastructure;
using System.Threading.Tasks;

public partial class Contact : Page
{
    protected async void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            serviceDAO service = new serviceDAO();
            try
            {
                await loadCategories(service, 0, 0);
            }
            catch (Exception Ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), Guid.NewGuid().ToString(), "alert('An error has ocurred, please contact with your software provider');", true);
            }
        }
    }


    protected async void gvCategories_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        serviceDAO service = new serviceDAO();
        try
        {
            List<Category> category = await service.GetCategory(Convert.ToInt32(gvCategories.Rows[Convert.ToInt32(e.CommandArgument)].Cells[0].Text));
            if (category != null && category.Count > 0)
            {
                if (e.CommandName == "editar")
                {
                    txtCategory.Text = category[0].Name;
                    btnUpdate.Text = "Update";
                    lblIDHidden.Text = category[0].Id.ToString();
                }
                else if (e.CommandName == "eliminar")
                {
                    List<Book> bookAssignments = await service.GetAssignmentsCategories(Convert.ToInt32(gvCategories.Rows[Convert.ToInt32(e.CommandArgument)].Cells[0].Text));
                    if (bookAssignments != null && bookAssignments.Count > 1)
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), Guid.NewGuid().ToString(), "alert('Invalid operation, The item is assigned to one or more books');", true);
                    else
                    {
                        service.DeleteCategory(category[0]);
                        await loadCategories(service, 0, 0);
                    }
                }
            }
        }
        catch (Exception Ex)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), Guid.NewGuid().ToString(), "alert('An error has ocurred, please contact with your software provider');", true);
        }
    }

    protected async void btnUpdate_Click(object sender, EventArgs e)
    {
        serviceDAO service = new serviceDAO();
        try
        {
            if (btnUpdate.Text == "Add")
            {
                Category category = new Category();
                category.Name = txtCategory.Text;
                service.AddCategory(category);

                await loadCategories(service, 0, 0);
                txtCategory.Text = string.Empty;
            }
            else
            {
                List<Category> category = await service.GetCategory(Convert.ToInt32(lblIDHidden.Text));
                category[0].Name = txtCategory.Text;
                service.UpdateCategory(category[0]);

                await loadCategories(service, 0, 0);
                txtCategory.Text = string.Empty;
                btnUpdate.Text = "Add";
            }
        }
        catch (Exception Ex)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), Guid.NewGuid().ToString(), "alert('An error has ocurred, please contact with your software provider');", true);
        }
    }

    private async Task loadCategories(serviceDAO service, int init, int currentPage)
    {
        List<Category> categorias = await service.GetAllCategories();
        int fin = init + gvCategories.PageSize;
        if (categorias != null && fin > categorias.Count)
            fin = categorias.Count;
        gvCategories.PageIndex = currentPage;

        gvCategories.DataSource = categorias;
        gvCategories.DataBind();
        for (int i = init; i < fin; i++)
            ((ImageButton)gvCategories.Rows[i].Cells[3].Controls[0]).Attributes.Add("OnClick", "if(!confirm('The entry will be deleted, are you sure?'))return false;");
    }

    protected async void gvCategories_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        serviceDAO service = new serviceDAO();
        try
        {
            await loadCategories(service, (gvCategories.PageSize * e.NewPageIndex), e.NewPageIndex);
        }
        catch (Exception Ex)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), Guid.NewGuid().ToString(), "alert('An error has ocurred, please contact with your software provider');", true);
        }
    }
}