using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Gpio;
using AppIOT.Drivers;

namespace AppIOT.Controllers
{
    class Startup
    {

        private const string I2C_CONTROLLER_NAME = "I2C1"; //use for RPI2
        private const byte DEVICE_I2C_ADDRESS = 0x27; // 7-bit I2C address of the port expander

        //Setup pins
        private const byte EN = 0x02;
        private const byte RW = 0x01;
        private const byte RS = 0x00;
        private const byte D4 = 0x04;
        private const byte D5 = 0x05;
        private const byte D6 = 0x06;
        private const byte D7 = 0x07;
        private const byte BL = 0x03;
        private DisplayI2c lcd;

        public Startup()
        {
            this.lcd = new DisplayI2c(DEVICE_I2C_ADDRESS, I2C_CONTROLLER_NAME, RS, RW, EN, D4, D5, D6, D7, BL);
        }

        public async Task Init()
        {
            lcd.init();
            lcd.prints("bom dia");
            lcd.gotoxy(0, 1);
            lcd.prints("Lcd test");

            var gpioController = await GpioController.GetDefaultAsync();


            try
            {


                var pin7 = gpioController.OpenPin(17);
                pin7.SetDriveMode(GpioPinDriveMode.Output);

                
                lcd.clrscr();
                lcd.prints("liga");
                pin7.Write(GpioPinValue.Low);
                await Task.Delay(1000);
                lcd.clrscr();
                lcd.prints("desliga");
                pin7.Write(GpioPinValue.High);
                await Task.Delay(1000);
                lcd.clrscr();
                lcd.prints("liga");
                pin7.Write(GpioPinValue.Low);
            }
            catch
            {
                Debug.Write("Erro desconhecido");
            }

            var mfrc = new Mfrc522();

            await mfrc.InitIO();

            while (true)
            {
                if (!mfrc.IsTagPresent()) continue;

                var uidd = mfrc.ReadUid();

                mfrc.HaltTag();

                lcd.clrscr();

                lcd.prints(uidd.ToString());

                await Task.Delay(1000);
            }


        }

        public async Task End()
        {
            lcd.prints("Finalizando...");
            await Task.Delay(2000);
            lcd.clrscr();
            lcd.turnOffBacklight();
        }

    }
}




