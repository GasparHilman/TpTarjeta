using Space;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_Tarjeta_tests
{
    internal class Iteracion3_tests
    {
        public TiempoFalso tiempo;
        public Colectivo k;
        public Tarjeta tarjeta;
        public Tarjeta medioBoleto;
        public Tarjeta gratuitoBoleto;

        [SetUp]
        public void Setup()
        {
            tiempo = new TiempoFalso();
            tarjeta = new Tarjeta(1);
            medioBoleto = new MedioBoleto(2);
            gratuitoBoleto = new GratuitoBoleto(3);
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

        [Test]
        public void MenosDe5Min_MedioBoleto()
        {
            tiempo.AgregarHoras(7);
            medioBoleto.Cargar_tarjeta(2000);
            k.PagarCon(medioBoleto, tiempo);
            tiempo.AgregarMinutos(1);
            k.PagarCon(medioBoleto, tiempo);
            Assert.That(medioBoleto.saldo, Is.EqualTo(2000 - (k.precio / 2) - k.precio));
        }
        [Test]
        public void cuatro_boletos_Medios()
        {
            tiempo.AgregarHoras(7);
            medioBoleto.Cargar_tarjeta(9000);
            k.PagarCon(medioBoleto, tiempo);
            tiempo.AgregarMinutos(10);
            k.PagarCon(medioBoleto, tiempo);
            tiempo.AgregarMinutos(10);
            k.PagarCon(medioBoleto, tiempo);
            tiempo.AgregarMinutos(10);
            k.PagarCon(medioBoleto, tiempo);
            tiempo.AgregarMinutos(10);
            Assert.That(medioBoleto.saldo, Is.EqualTo(9000 - ((k.precio / 2) * 4)));
            k.PagarCon(medioBoleto, tiempo);
            Assert.That(medioBoleto.saldo, Is.EqualTo(9000 - ((k.precio / 2) * 4) - k.precio));
        }
        [Test]
        public void dos_boletos_Gratuitos()
        {
            tiempo.AgregarHoras(7);
            gratuitoBoleto.Cargar_tarjeta(9000);
            k.PagarCon(gratuitoBoleto, tiempo);
            tiempo.AgregarMinutos(10);
            k.PagarCon(gratuitoBoleto, tiempo);
            tiempo.AgregarMinutos(10);
            Assert.That(gratuitoBoleto.saldo, Is.EqualTo(9000));
            k.PagarCon(gratuitoBoleto, tiempo);
            Assert.That(gratuitoBoleto.saldo, Is.EqualTo(9000 - k.precio));
        }

        [Test]
        public void MasDe5Min_MedioBoleto()
        {
            tiempo.AgregarHoras(7);
            medioBoleto.Cargar_tarjeta(9000);
            k.PagarCon(medioBoleto, tiempo);
            tiempo.AgregarMinutos(7);
            k.PagarCon(medioBoleto, tiempo);
            Assert.That(medioBoleto.saldo, Is.EqualTo(9000 - 2 * (k.precio / 2)));
        }
        [Test]
        public void PagoconCredito()
        {
            tarjeta.Cargar_tarjeta(9000);
            tarjeta.Cargar_tarjeta(9000);
            tarjeta.Cargar_tarjeta(9000);
            tarjeta.Cargar_tarjeta(9000);
            tarjeta.Cargar_tarjeta(3000);

            Assert.That(tarjeta.saldo, Is.EqualTo(tarjeta.saldo_max));
            Assert.That(tarjeta.credito, Is.EqualTo(39000 - tarjeta.saldo_max));
            k.PagarCon(tarjeta, tiempo);
            Assert.That(tarjeta.saldo, Is.EqualTo(tarjeta.saldo_max));
        }
        public void PagosinCredito()
        {
            tarjeta.Cargar_tarjeta(9000);
            tarjeta.Cargar_tarjeta(9000);
            tarjeta.Cargar_tarjeta(9000);
            tarjeta.Cargar_tarjeta(9000);

            Assert.That(tarjeta.saldo, Is.EqualTo(tarjeta.saldo_max));
            Assert.That(tarjeta.credito, Is.EqualTo(0));
            k.PagarCon(tarjeta, tiempo);
            Assert.That(tarjeta.saldo, Is.EqualTo(tarjeta.saldo_max - k.precio));
        }
    }
}
