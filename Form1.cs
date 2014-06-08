using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
//I added here date for registration and its output for validity,put richtextbox to display some errors like when textboxes are empty or with incorrect input
namespace DMV_GUI
{
    public partial class Form1 : Form
    {

        MotorVehicle[] vehicleArray = new MotorVehicle[20]; //Define Array of MotorVehicle objects
        int mvArrayCounter = 0; //Initialize array counter  
        public static string textFile = "log-"+(DateTime.Now.ToString("dd-MM-yyyy"))+".txt"; //Define dynamic time-dependant name of textfile

        public Form1()
        {
            InitializeComponent();
        }

        private void onLoad(object sender, EventArgs e)
        {
            VehicleTypeChange(null, null); //Run function to check the default radio button

            if (!File.Exists(textFile)) //Check if our textfile already exists
            {
                FileStream fileStream = new FileStream(textFile, FileMode.Create, FileAccess.Write); //Create a new texfile
                fileStream.Close(); //Close the file, so that other methods can acces it
            }
        }

        private void VehicleTypeChange(object sender, EventArgs e) //Method for radio button selector. Displays required fileds for diferent types of motor Vehicles
        {
            if (rbTruck.Checked)
            {
                customLabel01.Visible = customTb01.Visible = true;
                customLabel02.Visible = customTb02.Visible = customLabel03.Visible = rbYes.Visible = rbNo.Visible = false;
                customLabel01.Text = "maximum weight";

                ComboBoxVehicleManufacturer.Items.Clear();
                ComboBoxVehicleManufacturer.Items.Add("MAN");
                ComboBoxVehicleManufacturer.Items.Add("Volvo");
                ComboBoxVehicleManufacturer.Items.Add("Mercedes");
                ComboBoxVehicleManufacturer.Items.Add("Ford");
                ComboBoxVehicleManufacturer.Items.Add("Chevrolet");
                ComboBoxVehicleManufacturer.Sorted = true;

                ComboBoxVehicleManufacturer.SelectedIndex = 0;
            }
            else if (rbBus.Checked)
            {
                customLabel01.Visible = customTb01.Visible = true;
                customLabel02.Visible = customTb02.Visible = customLabel03.Visible = rbYes.Visible = rbNo.Visible = false;
                customLabel01.Text = "Company name";

                ComboBoxVehicleManufacturer.Items.Clear();
                ComboBoxVehicleManufacturer.Items.Add("Neoplan");
                ComboBoxVehicleManufacturer.Items.Add("MAN");
                ComboBoxVehicleManufacturer.Items.Add("Volvo");
                ComboBoxVehicleManufacturer.Items.Add("Iveco");
                ComboBoxVehicleManufacturer.Items.Add("Hyundai");

                ComboBoxVehicleManufacturer.Sorted = true;
                ComboBoxVehicleManufacturer.SelectedIndex = 0;
            }
            else if (rbCar.Checked)
            {
                customLabel01.Visible = customTb01.Visible = customLabel02.Visible = customLabel03.Visible = customTb02.Visible = rbYes.Visible = rbNo.Visible = true;
                customLabel01.Text = "Car Color";
                customLabel02.Text = "Number of airbags";
                customLabel03.Text = "Does the car have AC?";

                ComboBoxVehicleManufacturer.Items.Clear();
                ComboBoxVehicleManufacturer.Items.Add("Ferrari");
                ComboBoxVehicleManufacturer.Items.Add("Audi");
                ComboBoxVehicleManufacturer.Items.Add("BMW");
                ComboBoxVehicleManufacturer.Items.Add("Volkswagen");
                ComboBoxVehicleManufacturer.Items.Add("Mercedes");
                ComboBoxVehicleManufacturer.Items.Add("Volvo");
                ComboBoxVehicleManufacturer.Items.Add("Ford");

                ComboBoxVehicleManufacturer.Sorted = true;
                ComboBoxVehicleManufacturer.SelectedIndex = 0;
            }
            else if (rbTaxi.Checked)
            {
                customLabel01.Visible = customTb01.Visible = customLabel02.Visible = customLabel03.Visible = customTb02.Visible = rbYes.Visible = rbNo.Visible = customLabel04.Visible = rbYes2.Visible = rbNo2.Visible = true;
                customLabel01.Text = "Car Color";
                customLabel02.Text = "Number of airbags";
                customLabel03.Text = "Cab has AC?";
                customLabel04.Text = "Driver has licence?";

                ComboBoxVehicleManufacturer.Items.Clear();
                ComboBoxVehicleManufacturer.Items.Add("Ferrari");
                ComboBoxVehicleManufacturer.Items.Add("Audi");
                ComboBoxVehicleManufacturer.Items.Add("BMW");
                ComboBoxVehicleManufacturer.Items.Add("Volkswagen");
                ComboBoxVehicleManufacturer.Items.Add("Mercedes");
                ComboBoxVehicleManufacturer.Items.Add("Volvo");
                ComboBoxVehicleManufacturer.Items.Add("Ford");

                ComboBoxVehicleManufacturer.Sorted = true;
                ComboBoxVehicleManufacturer.SelectedIndex = 0;
            }
        }

        private void RegisterVehicleClick(object sender, EventArgs e) //Button Click method. Creates objects, puts them in our array, then displays them in log as well as stores them in out textfile
        {
            rtLog.Clear();
            MotorVehicle mv = null;
            if (dateTimePicker1.Value < datePicker.Value) { errorbox.AppendText("\n Date of car production cant be after date of its registration"); }//added to check the dates
            else if (String.IsNullOrEmpty(tbVIN.Text) || String.IsNullOrEmpty(tbModel.Text) || String.IsNullOrEmpty(customTb01.Text))//I have added this part cause previously there was nothing to check if those textboxes were empty
            {
                errorbox.Clear();
                errorbox.AppendText("\n You didnt fill in all boxes.");
            }
            else if (rbTruck.Checked)
            {
                double i;
                if (!double.TryParse(customTb01.Text, out i)) { errorbox.AppendText("\n incorrect weight input"); }//I added this to check double input for weight so it doesnt throw exception
                else{
                mv = new Truck(dateTimePicker1.Value,tbVIN.Text, ComboBoxVehicleManufacturer.Text, tbModel.Text, (int)NoOfWheels.Value, (int)NoOfSeats.Value, datePicker.Value, Convert.ToDouble(customTb01.Text));
            }
            }
            else if (rbBus.Checked)
            {
                mv = new Bus(dateTimePicker1.Value, tbVIN.Text, ComboBoxVehicleManufacturer.Text, tbModel.Text, (int)NoOfWheels.Value, (int)NoOfSeats.Value, datePicker.Value, customTb01.Text);                
            }
            else if (rbCar.Checked)
            {
                int i;
                if (String.IsNullOrEmpty(customTb02.Text) && !Int32.TryParse(customTb02.Text,out i)){errorbox.AppendText("1 box isnt filled correctly");}//I added so there isnt error if box isnt filled or if it contains incorrect input
                else{
                    mv = new Car(dateTimePicker1.Value, tbVIN.Text, ComboBoxVehicleManufacturer.Text, tbModel.Text, (int)NoOfWheels.Value, (int)NoOfSeats.Value, datePicker.Value, customTb01.Text, rbYes.Checked, Convert.ToInt32(customTb02.Text));
                }
            }
            else if (rbTaxi.Checked)
            {
                int i;
                if (String.IsNullOrEmpty(customTb02.Text) && !Int32.TryParse(customTb02.Text, out i)) { errorbox.AppendText("1 box isnt filled correctly"); }//added here too
                else
                {
                    mv = new Taxi(dateTimePicker1.Value, tbVIN.Text, ComboBoxVehicleManufacturer.Text, tbModel.Text, (int)NoOfWheels.Value, (int)NoOfSeats.Value, datePicker.Value, customTb01.Text, rbYes.Checked, Convert.ToInt32(customTb02.Text), rbYes2.Checked);
                }
            }

            vehicleArray[mvArrayCounter++] = mv; //Append newest object to array
            
            rtLog.Clear();

            foreach(MotorVehicle m in vehicleArray) //Display and store in textfile
            {
                if (m != null)
                {
                    rtLog.AppendText(m.show() + "\n\n");
                    using (FileStream file = new FileStream(textFile, FileMode.Append, FileAccess.Write))
                    {
                        using (StreamWriter writer = new StreamWriter(file))
                        {
                            writer.WriteLine(m.show());
                            writer.Close();
                        }
                        file.Close();  
                    }
                    
                }
            }
        }
        
        private void ShowLastVehicleFromFile(object sender, EventArgs e) //Get from textfile and display in Richtextbox
        {
            using (FileStream fileStream = new FileStream(textFile, FileMode.Open, FileAccess.Read))
            {
                using (StreamReader streamReader = new StreamReader(fileStream))
                {
                    string allFileLines;
                    string[] currentLine = new String[20];
                    int lineCounter = 0;

                    while((allFileLines = streamReader.ReadLine()) != null) {
                        try
                        {
                            currentLine[lineCounter++] = allFileLines;
                        }
                        catch
                        {
                            break;
                        }
                    }
                    if (lineCounter >= 1)//I added this to prevent error if file is empty
                    {
                        rtLog.AppendText(lineCounter.ToString() + ": " + currentLine[lineCounter - 1] + "\n\n");
                    }
                    streamReader.Close();
                }
                fileStream.Close(); 
            }        
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }        
    }
}
