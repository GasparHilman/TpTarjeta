using NUnit.Framework;
using Space;

namespace TP_Tarjeta_tests
{
    public class Tests
    {
        public Colectivo k;
        public Tarjeta tarjeta;
        public Tarjeta medioBoleto;
        public Tarjeta gratuitoBoleto;

        [SetUp]
        public void Setup()
        {
            tarjeta = new Tarjeta(1);
            medioBoleto = new MedioBoleto(2);
            gratuitoBoleto = new GratuitoBoleto(3);
            k = new Colectivo("K");
        }

        [Test]
        public void chequeo_ingresos_saldos_posibles()
        {
            tarjeta.Cargar_tarjeta(2000);
            Assert.That(tarjeta.saldo, Is.EqualTo(2000));

            tarjeta.saldo = 0;

            tarjeta.Cargar_tarjeta(3000);
            Assert.That(tarjeta.saldo, Is.EqualTo(3000));

            tarjeta.saldo = 0;

            tarjeta.Cargar_tarjeta(4000);
            Assert.That(tarjeta.saldo, Is.EqualTo(4000));

            tarjeta.saldo = 0;

            tarjeta.Cargar_tarjeta(5000);
            Assert.That(tarjeta.saldo, Is.EqualTo(5000));

            tarjeta.saldo = 0;

            tarjeta.Cargar_tarjeta(6000);
            Assert.That(tarjeta.saldo, Is.EqualTo(6000));

            tarjeta.saldo = 0;

            tarjeta.Cargar_tarjeta(7000);
            Assert.That(tarjeta.saldo, Is.EqualTo(7000));

            tarjeta.saldo = 0;

            tarjeta.Cargar_tarjeta(8000);
            Assert.That(tarjeta.saldo, Is.EqualTo(8000));

            tarjeta.saldo = 0;

            tarjeta.Cargar_tarjeta(9000);
            Assert.That(tarjeta.saldo, Is.EqualTo(9000));

        }

        [Test]
        public void chequeo_pagar_con_y_sin_saldo_negativo()
        {
            tarjeta.Cargar_tarjeta(9000);
            k.PagarCon(tarjeta);
            Assert.That(tarjeta.saldo, Is.EqualTo(8060));

            tarjeta.saldo = 0;

            k.PagarCon(tarjeta);
            Assert.That(tarjeta.saldo, Is.EqualTo(0));

            tarjeta.saldo = 500;
            k.PagarCon(tarjeta);
            Assert.That(tarjeta.saldo, Is.EqualTo(500 - k.precio));

            tarjeta.Cargar_tarjeta(2000);
            Assert.That(tarjeta.saldo, Is.EqualTo(500 - k.precio + 2000));

        }

        [Test]
        public void chequeo_que_pueda_pagar_inifinitamente_gratuitoBoleto_y_medioBoleto()
        {
            gratuitoBoleto.saldo = 900;
            for (int i = 0; i < 20; i++) {
                k.PagarCon(gratuitoBoleto);
            }
            Assert.That(gratuitoBoleto.saldo, Is.EqualTo(900));

            medioBoleto.saldo = 3000;
            for (int i = 0; i < 3; i++)
            {
                k.PagarCon(medioBoleto);
                Assert.That(medioBoleto.saldo, Is.EqualTo(medioBoleto.saldo - (k.precio/2)));
            }
        }
        }
}