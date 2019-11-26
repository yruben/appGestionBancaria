﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App_Gestion_Bancaria.Core.Gestores;
using App_Gestion_Bancaria.Core.Clases;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyectos.Ui
{
    public class CuentaController
    {
        public CuentaController()
        {
            this.Gestor = new GestorCuentas();
            this.View = new CuentaView(this.Gestor);
            this.IniciarBotonesIndex();
        }

        private void IniciarBotonesIndex()
        {
            this.View.ButtonDetalle.Click += new System.EventHandler(AccionButtonDetalle);
            this.View.AddCuentaButton.Click += new System.EventHandler(AddCuenta);
        }

        private void IniciarBotonesDetalles()
        {
            this.View.ButtonVolver.Click += new System.EventHandler(AccionButtonVolver);
            this.View.AddRetiradaButton.Click += new System.EventHandler(AddRetirada);
            this.View.AddDepositoButton.Click += new System.EventHandler(AddDeposito);
            this.View.GuardarButton.Click += new System.EventHandler(CambiarCuenta);
        }

        private void IniciarBotonesAddCuenta()
        {
            this.View.SaveCuentaButton.Click += new System.EventHandler(SaveCuenta);
        }

        private void AddCuenta(object sender, System.EventArgs e)
        {
            this.View.ShowAddCuenta(this.Gestor);
            this.IniciarBotonesAddCuenta();
        }

        private void SaveCuenta(object sender, System.EventArgs e)
        {

        }

        private void CambiarCuenta(object sender, System.EventArgs e)
        {
            this.View.ShowIndex(this.Gestor);
            this.IniciarBotonesIndex();
        }

        private void AccionButtonVolver(object sender, System.EventArgs e)
        {
            Console.WriteLine("VOLVER");
            this.View.ShowIndex(this.Gestor);
            this.IniciarBotonesIndex();
        }

        private void AddRetirada(object sender, System.EventArgs e)
        {
            this.View.ShowAddMovimiento(true);
            this.View.AddRetiradaButton.Click -= new System.EventHandler(AddRetirada);
            this.View.AddRetiradaButton.Click += new System.EventHandler(NewRetirada);
        }

        private void NewRetirada(object sender, System.EventArgs e)
        {
            this.CuentaSeleccionada.Retiradas.Add(new Movimiento(Decimal.ToInt32(this.View.CantidadNumeric.Value), new Cliente("", this.View.ClienteTextBox.Text, "", "", ""), this.View.FechaMovimientoDate.Value));
            this.CuentaSeleccionada.Saldo -= (int) this.View.CantidadNumeric.Value;
            this.View.ShowDetalles(this.CuentaSeleccionada);
            this.View.AddRetiradaButton.Click -= new System.EventHandler(NewRetirada);
            this.IniciarBotonesDetalles();
        }

        private void AddDeposito(object sender, System.EventArgs e)
        {
            this.View.ShowAddMovimiento(false);
            this.View.AddDepositoButton.Click -= new System.EventHandler(AddDeposito);
            this.View.AddDepositoButton.Click += new System.EventHandler(NewDeposito);
            this.View.ButtonVolver.Click += new System.EventHandler(AccionButtonVolverMovimiento);
        }

        private void NewDeposito(object sender, EventArgs e)
        {
            this.CuentaSeleccionada.Depositos.Add(new Movimiento(Decimal.ToInt32(View.CantidadNumeric.Value), new Cliente("", this.View.ClienteTextBox.Text, "", "", ""), this.View.FechaMovimientoDate.Value));
            this.CuentaSeleccionada.Saldo += (int)this.View.CantidadNumeric.Value;
            this.View.ShowDetalles(this.CuentaSeleccionada);
            this.View.AddDepositoButton.Click -= new System.EventHandler(NewDeposito);
            this.IniciarBotonesDetalles();
        }

        private void AccionButtonVolverMovimiento(object sender, EventArgs e)
        {
            this.View.ShowDetalles(this.CuentaSeleccionada);
            this.IniciarBotonesDetalles();
        }

        private void AccionButtonDetalle(object sender, System.EventArgs e)
        {
            DataGridViewSelectedRowCollection filasSeleccionadas = this.View.TablaCuentas.SelectedRows;
            if(filasSeleccionadas.Count != 1)
            {
                MessageBox.Show("Selecciona una única cuenta", "Error de selección", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            } else
            {
                this.CuentaSeleccionada = Gestor.Cuentas[filasSeleccionadas[0].Index];
                this.View.ShowDetalles(this.CuentaSeleccionada);
                this.IniciarBotonesDetalles();
            }
        }

        public CuentaView View { get; set; }

        public GestorCuentas Gestor { get; set; }

        public Cuenta CuentaSeleccionada { get; set; }

    }

}