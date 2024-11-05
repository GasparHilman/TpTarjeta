using Space;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_Tarjeta_tests
{
    internal class Boleto_tests
    {
        public TiempoFalso tiempo;
        public Colectivo k;
        public Tarjeta tarjeta;
        public Tarjeta medioBoleto;
        public Tarjeta gratuitoBoleto;
        public Tarjeta jubiladoboleto;

        [SetUp]
        public void Setup()
        {
            tiempo = new TiempoFalso();
            tarjeta = new Tarjeta(1);
            medioBoleto = new MedioBoleto(2);
            gratuitoBoleto = new GratuitoBoleto(3);
            jubiladoboleto = new JubiladoBoleto(4);
            k = new Colectivo("K");
        }

        [Test]
        public void mostrarBoleto_TarjetaNormal()
        {
            tarjeta.Cargar_tarjeta(2000);
            k.PagarCon(tarjeta, tiempo);
            tarjeta.historial.Last().mostrarboleto();
        }

        [Test]
        public void mostrarBoleto_TarjetaMedioBoleto()
        {
            tiempo.AgregarHoras(7);
            medioBoleto.Cargar_tarjeta(2000);
            k.PagarCon(medioBoleto, tiempo);
            medioBoleto.historial.Last().mostrarboleto();
        }

        [Test]
        public void mostrarBoleto_TarjetaGratuitoBoleto()
        {
            tiempo.AgregarHoras(7);
            gratuitoBoleto.Cargar_tarjeta(2000);
            k.PagarCon(gratuitoBoleto, tiempo);
            gratuitoBoleto.historial.Last().mostrarboleto();
        }

        public void mostrarBoleto_TarjetaJubiladoBoleto()
        {
            tiempo.AgregarHoras(7);
            jubiladoboleto.Cargar_tarjeta(2000);
            k.PagarCon(jubiladoboleto, tiempo);
            jubiladoboleto.historial.Last().mostrarboleto();
        }

    }
}
