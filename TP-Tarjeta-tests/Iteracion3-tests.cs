using NUnit.Framework;
using Space;
using TP_Tarjeta;



namespace TP_Tarjeta_tests
{
    public class Tests1
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
            k.PagarCon(tarjeta, tiempo);
            Assert.That(tarjeta.saldo, Is.EqualTo(8060));

            tarjeta.saldo = 0;

            k.PagarCon(tarjeta, tiempo);
            Assert.That(tarjeta.saldo, Is.EqualTo(0));

            tarjeta.saldo = 500;
            k.PagarCon(tarjeta, tiempo);
            Assert.That(tarjeta.saldo, Is.EqualTo(500 - k.precio));

            tarjeta.Cargar_tarjeta(2000);
            Assert.That(tarjeta.saldo, Is.EqualTo(500 - k.precio + 2000));

        }

        [Test]
        public void test3()
        {
            tarjeta.saldo = 2000;
            k.PagarCon(tarjeta, tiempo);
            tarjeta.historial.Last().mostrarboleto();

            medioBoleto.saldo = 2000;
            k.PagarCon(medioBoleto, tiempo);
            medioBoleto.historial.Last().mostrarboleto();

            gratuitoBoleto.saldo = 2000;
            k.PagarCon(gratuitoBoleto, tiempo);
            gratuitoBoleto.historial.Last().mostrarboleto();
        }

        [Test]
        public void test4()
        {

            medioBoleto.saldo = 2000;
            k.PagarCon(medioBoleto, tiempo);
            tiempo.AgregarMinutos(1);
            k.PagarCon(medioBoleto, tiempo);
            Assert.That(medioBoleto.saldo, Is.EqualTo(2000 - (k.precio / 2) - k.precio));


            tiempo.AgregarMinutos(7);
            medioBoleto.saldo = 2000;
            k.PagarCon(medioBoleto, tiempo);
            tiempo.AgregarMinutos(7);
            k.PagarCon(medioBoleto, tiempo);
            Assert.That(medioBoleto.saldo, Is.EqualTo(2000 - k.precio));

            tiempo.AgregarMinutos(7);
            medioBoleto.saldo = 2000;
            k.PagarCon(medioBoleto, tiempo);
            Assert.That(medioBoleto.saldo, Is.EqualTo(2000 - k.precio));
        }

        [Test]
        public void test5()
        {

            gratuitoBoleto.saldo = 2000;
            k.PagarCon(gratuitoBoleto, tiempo);
            k.PagarCon(gratuitoBoleto, tiempo);
            Assert.That(gratuitoBoleto.saldo, Is.EqualTo(2000));
            k.PagarCon(gratuitoBoleto, tiempo);
            Assert.That(gratuitoBoleto.saldo, Is.EqualTo(2000 - k.precio));

         
        }

        [Test]
        public void test6()
        {

            tarjeta.saldo = 30000;
            tarjeta.Cargar_tarjeta(8000);
   
            Assert.That(tarjeta.saldo, Is.EqualTo(tarjeta.saldo_max));
            Assert.That(tarjeta.credito, Is.EqualTo(38000 - tarjeta.saldo_max));

            k.PagarCon(tarjeta, tiempo);
            Assert.That(tarjeta.saldo, Is.EqualTo(tarjeta.saldo_max));

        }

    }
}