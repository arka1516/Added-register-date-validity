using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMV_GUI
{
    abstract class MotorVehicle
    {
        string VIN;
        string make;
        string model;
        DateTime dateOfProduction;
        DateTime dateOfRegistration;
        protected int noOfWheels;
        protected int noOfSeats;
        protected char fieldSep = '|';
        protected string valid(DateTime dateOfProduction,DateTime dateOfRegistration)
        {
//I added this to check if registration date was valid
            if (dateOfRegistration > DateTime.Today) { return "Not registered yet"; }
                else if (dateOfRegistration.AddYears(1) < DateTime.Today) { return "Not valid"; }
                else { return "Valid"; }
            
        }
        public MotorVehicle(DateTime register,string VIN, string make, string model, int noOfWheels, int noOfSeats, DateTime dateOfProduction)
        {
            this.dateOfRegistration = register;
            this.VIN = VIN;
            this.make = make;
            this.model = model;
            this.noOfSeats = noOfSeats;
            this.noOfWheels = noOfWheels;
            this.dateOfProduction = dateOfProduction;
        }

        public virtual string show()
        {
            return string.Format(" Make: {1} {0} Model: {2} {0} Number of Wheels: {3} Valid registration: {4}", fieldSep, make, model, noOfWheels,valid(dateOfProduction,dateOfRegistration));
        }
    }

    class Truck : MotorVehicle
    {
        private double maxWeight;

        public Truck(DateTime register, string VIN, string make, string model, int noOfWheels, int noOfSeats, DateTime dateOfProduction, double maxWeight)
            : base(register,VIN, make, model, noOfSeats, noOfWheels, dateOfProduction)
        {
            this.maxWeight = maxWeight;
        }

        public override string show()
        {
            return string.Format("Truck: " + base.show() +fieldSep+ "Maximum Weight:"+maxWeight);

        }
    }

    //has to have >8 seats to be a bus
    class Bus : MotorVehicle
    {
        private string companyName;

        public Bus(DateTime register, string VIN, string make, string model, int noOfWheels, int noOfSeats, DateTime dateOfProduction, string companyName)
            : base(register, VIN, make, model, noOfSeats, noOfWheels, dateOfProduction)
        {
            this.companyName = companyName;
        }

        public override string show()
        {
            return string.Format("Bus: " + base.show() + " {0} Company Name: {1}", fieldSep, companyName);
        }
    }

    //has to have <8 seats to be a car
    class Car : MotorVehicle
    {
        private string color;
        private bool AC;
        private int airbags;

        public Car(DateTime register, string VIN, string make, string model, int noOfWheels, int noOfSeats, DateTime dateOfProduction, string color, bool AC, int airbags)
            : base(register, VIN, make, model, noOfSeats, noOfWheels, dateOfProduction)
        {
            this.color = color;
            this.AC = AC;
            this.airbags = airbags;
        }

        public override string show()
        {
            return string.Format("Car: " + base.show() + " {0} Color: {1} {0} Has AC: {2} {0} Number of Airbags: {3}", fieldSep, color, AC, airbags);
        }
    }

    class Taxi : Car
    {
        private bool licence;

        public Taxi(DateTime register, string VIN, string make, string model, int noOfWheels, int noOfSeats, DateTime dateOfProduction, string color, bool AC, int airbags, bool licence)
            : base(register, VIN, make, model, noOfWheels, noOfSeats, dateOfProduction, color, AC, airbags)
        {
            //new Car(register, valid, VIN, make, model, noOfSeats, noOfWheels, dateOfProduction, color, AC, airbags);
            //i commented this upper part as I read that you cant call constructor inside other constructor like that
            this.licence = licence;
        }

        public override string show()
        {
            return string.Format("Taxi: " + base.show() + "{0} Driver has licence: {1}", fieldSep, licence);
        }
    }

    class Motorcycle : MotorVehicle
    {
        private double ccm;

        public Motorcycle(DateTime register, string VIN, string make, string model, int noOfWheels, int noOfSeats, DateTime dateOfProduction, double ccm)
            : base(register, VIN, make, model, noOfSeats, noOfWheels, dateOfProduction)
        {
            this.ccm = ccm;
        }

        public override string show()
        {
            return string.Format("Motorcycle: " + base.show() + " {0} CCM: {1} ", fieldSep, ccm);
        }
    }
}
