﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;
using Negocio;

namespace Carrito_de_Compras
{
    public partial class WebForm1 : System.Web.UI.Page
    {


        
        Producto producto = new Producto();
        public Carrito carrito = new Carrito();
        ItemCarrito item = new ItemCarrito();
        decimal total;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string id = Request.QueryString["id"];
                carrito = (Carrito)Session["carrito"];
                if (carrito == null) carrito = new Carrito();
               // carrito.Items = (List<ItemCarrito>)Session["carrito"];
                if (carrito.Items == null) carrito.Items = new List<ItemCarrito>();



                if (!IsPostBack)
                {
                    if (id != null)
                    {
                        if (carrito.Items.Find(x => x.Producto.Id.ToString() == id) == null)
                        {
                            List<Producto> listado = (List<Producto>)Session["listadoProductos"];
                            producto = listado.Find(x => x.Id.ToString() == id);

                            item.SubTotal = producto.Precio;
                            item.Producto = producto;
                            item.Cantidad = 1;
                            carrito.FechaCarrito = DateTime.Today;
                            //itemCarritos.Items = (List<ItemCarrito>)Session["carrito"];
                            carrito.Items.Add(item);

                        }
                    }
                    //Repeater
                    repetidor.DataSource = carrito.Items;
                    repetidor.DataBind();
                }
                lblTotal.Text = carrito.totalCarrito(carrito).ToString();
                Session.Add("carrito", carrito);
            }
            catch (Exception ex)
            {

                Session.Add("Error", ex.ToString());
                Response.Redirect("Error.aspx");
            }

        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                var argument = ((Button)sender).CommandArgument;
                //carrito.Items = (List<ItemCarrito>)Session["carrito"];
                carrito = (Carrito)Session["carrito"];
                ItemCarrito item1 = carrito.Items.Find(x => x.Producto.Id.ToString() == argument);
                carrito.Items.Remove(item1);
                Session.Add("carrito", carrito);
                repetidor.DataSource = null;
                repetidor.DataSource = carrito.Items;
                repetidor.DataBind();
                lblTotal.Text = carrito.totalCarrito(carrito).ToString();
            }
            catch (Exception ex)
            {

                Session.Add("Error", ex.ToString());
                Response.Redirect("Error.aspx");
            }
        }

        protected void txtCantidad_TextChanged(object sender, EventArgs e)
        {
            try
            {
                var cantidad = ((TextBox)sender).Text;
                lblTotal.Text = cantidad;
            
            
            }

            catch (Exception ex)
            {

                Session.Add("Error", ex.ToString());
                Response.Redirect("Error.aspx");
            }


        }

        protected void lblTotal_Load(object sender, EventArgs e)
        {
            
            
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                var cantidad = lblTotal.Text;
                if (cantidad != "")
                {
                    var argument = ((Button)sender).CommandArgument;
                    carrito = (Carrito)Session["carrito"];
                    ItemCarrito item1 = carrito.Items.Find(x => x.Producto.Id.ToString() == argument);
                    item1.Cantidad = int.Parse(cantidad);
                    Session.Add("carrito", carrito);
                    repetidor.DataSource = null;
                    repetidor.DataSource = carrito.Items;
                    repetidor.DataBind();
                    lblTotal.Text = carrito.totalCarrito(carrito).ToString();
                }
            }
            catch (Exception ex)
            {

                Session.Add("Error", ex.ToString());
                Response.Redirect("Error.aspx");
            }

        }
        
    }
}