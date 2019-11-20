﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using App_Gestion_Bancaria.Core.Gestores;
using App_Gestion_Bancaria.Core.Clases;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyectos.Ui
{
    using WFrms = System.Windows.Forms;
    public class ClienteController
    {

        public ClienteView View { get; private set; }
        public GestorClientes Gestor { get; private set; }

        public ClienteController()
        {
            this.Gestor = new GestorClientes();
            this.Gestor.RecuperarClientes();
            this.View = new ClienteView(this.Gestor);
            this.IniciarBotones();
        }

        private void IniciarBotones()
        {
            this.View.botonAddCliente.Click += new System.EventHandler(accionAddCliente);
            this.View.botonDeleteCliente.Click += new System.EventHandler(accionDeleteCliente);
        }


        private void accionAddCliente(object sender, System.EventArgs e)
        {
            this.View.BuiltAddCliente();
            this.View.BotonAdd.Click += new System.EventHandler(accionAdd);
            this.View.BotonVolver.Click += new System.EventHandler(accionVolver);
        }

        private void accionAdd(object sender, System.EventArgs e)
        {
            string dniRecuperado = this.View.Dni.Text;
            string telefonoRecuperado = this.View.Telefono.Text;
            string emailRecuperado = this.View.Email.Text;
            string nombreRecuperado = this.View.Nombre.Text;
            string direccionPostalRecuperada = this.View.DireccionPostal.Text;

            Boolean noExiste = this.Gestor.Insertar(dniRecuperado, nombreRecuperado, telefonoRecuperado, emailRecuperado, direccionPostalRecuperada);

            if (!noExiste)
            {
                this.View.BuiltError("Los campos DNI, telefono y email deben ser únicos", this.Gestor);
                this.IniciarBotones();
            }
            else
            {
                this.View.ClienteViewMethod(this.Gestor);
                this.IniciarBotones();
            }

        }

        private void accionVolver(object sender, System.EventArgs e)
        {
            this.View.ClienteViewMethod(this.Gestor);
            this.IniciarBotones();
        }


        private void accionDeleteCliente(object sender, System.EventArgs e)
        {
            DataGridViewSelectedRowCollection filasSeleccionadas = this.View.TablaClientes.SelectedRows;
            int indiceTabla = filasSeleccionadas[0].Index;

            Cliente clienteSeleccionado = this.Gestor.ContenedorClientes[indiceTabla];

            if (this.View.BuiltDeleteCliente("¿Seguro que desea eliminar el cliente con DNI " + clienteSeleccionado.Dni
                + " ?"))
            {
                this.Gestor.Eliminar(clienteSeleccionado.Dni);
            }
            this.View.ClienteViewMethod(this.Gestor);
            this.IniciarBotones();
        }
    }
}
