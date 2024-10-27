using NUnit.Framework;
using Space;


namespace TP_Tarjeta_tests
{
    public class Test_siteracion4
    {
        public TiempoFalso tiempo;
        public Colectivo k;
        public Tarjeta tarjeta;
        //public Tarjeta medioBoleto;
        //public Tarjeta gratuitoBoleto;

        [SetUp]
        public void Setup()
        {
            tiempo = new TiempoFalso();
            tarjeta = new Tarjeta(1);
            //medioBoleto = new MedioBoleto(2);
            //gratuitoBoleto = new GratuitoBoleto(3);
            k = new Colectivo("K");
        }

        [Test]
        public void uso_frecuente()
        {
            tarjeta.viajesmes = 28;
            tarjeta.Cargar_tarjeta(9000);
            k.PagarCon(tarjeta,tiempo); //boleto 29
            Assert.That(tarjeta.saldo, Is.EqualTo(9000-k.precio));
            Assert.That(tarjeta.viajesmes, Is.EqualTo(29));
            k.PagarCon(tarjeta, tiempo); //boleto 30
            Assert.That(tarjeta.saldo, Is.EqualTo(9000 - k.precio - ((int)(k.precio*0.8)) ));
            Assert.That(tarjeta.viajesmes, Is.EqualTo(30));
            tarjeta.viajesmes = 79;
            k.PagarCon(tarjeta, tiempo); //boleto 80
            Assert.That(tarjeta.saldo, Is.EqualTo(9000 - k.precio - ((int)(k.precio * 0.8)) - ((int)(k.precio * 0.75))));
            k.PagarCon(tarjeta, tiempo); //boleto 81
            Assert.That(tarjeta.saldo, Is.EqualTo(9000 - k.precio*2 - ((int)(k.precio * 0.8)) - ((int)(k.precio * 0.75))));
        }

    }
}