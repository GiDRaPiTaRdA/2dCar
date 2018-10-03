using System;
using UnityEngine;

public class Vehicle
{
    private const double acceleration = 0.01;
    private const double brake = 0.5;
    private const double maxWheelAngle = 0.62; // Rad 0.62
    private const double friction = 0.005;

	private double wheelAngle = 0;

    public Vector3 Origin { get; private set; }
    public Vector3 Size { get; private set; }

    public double Speed { get; private set; }
	public double RelativeSpeed { get { return this.Speed / ScaleFunction(); } }
    public double Direction { get; private set; }   // Rad
	public double WheelAngle {
		get{
			return this.wheelAngle;
		}
		private set{
			if (value > maxWheelAngle) {
				this.wheelAngle = maxWheelAngle;
			} else if (value < -maxWheelAngle) {
				this.wheelAngle = -maxWheelAngle;
			} else {
				this.wheelAngle = value;
			}
		}
	}// Rad
    public double TurnAngle { get; private set; }  // Rad

    public Vehicle(Vector3 origin, Vector3 size)
    {
        this.Origin = origin;
        this.Size = size;
        this.WheelAngle = 0;
        this.TurnAngle = 0;
        this.Direction = 0;
        this.Speed = 0;
    }

	public void Action()
    {
        //friction
        //slows the vehicle down all time a litlle
        this.Speed += FrictionFunction(this.RelativeSpeed);

        // go to aimed direction
        //Vector3 change = new Vector3((float)(Math.Sin(this.Direction) * this.Speed), (float)(Math.Cos(this.Direction) * this.Speed));
        //.Origin = new Vector3(this.Origin.x + change.x, this.Origin.y + change.y);

        this.Turn (this.WheelAngle);


    }

    public void Accelerate()
    {
        this.Speed += AccelerationFunction(this.Speed) * 0.1 * ScaleFunction();
    }

    public void AccelerateBack()
    {
        this.Speed -= acceleration * ScaleFunction();
    }

    public void Brake()
    {
        if (this.Speed > brake)
        {
            this.Speed -= brake;
        }
        else if (this.Speed < -brake)
        {
            this.Speed += brake;
        }
        else
        {
            this.Speed = 0;
        }
    }

    private void Turn(double angle)
    {        
        if (this.WheelAngle != 0)
        {
            this.TurnAngle = TurnFunction(angle, RelativeSpeed);

            // Apply shift
            Vector3 shiftVector = WheelsShift();
            this.Origin = new Vector3(this.Origin.x + shiftVector.x, this.Origin.y + shiftVector.y);

            // Apply direction
            this.Direction += Math.Abs(angle) * this.TurnAngle * ValueSignDependency(this.Speed);
        }
    }

	public void TurnLeft() { this.WheelAngle = -maxWheelAngle; }
	public void TurnRight() {this.WheelAngle = maxWheelAngle; }
	public void SetWheelAngle(double angle) {this.WheelAngle = angle;
	}

    private double TurnFunction(double angle, double speed)
	{
        // Giper parameters
        double z = 1; double h = 0.1; double w = 1;

        double x = angle;
        double y = z * h;

        if (y >= h || y <= -h)
        {
            //y = (-Math.Sqrt(-x * w + Math.Pow(z, 2)) + z) * h;
            //y= Math.Sqrt(-Math.Pow((x*w-Math.Pow(z,2)),2)+Math.Pow(z,2))*h;
            //y = (Math.Cos(Math.PI*(x*w+1))+1)/2*h;
            //y = x*w*h;
            y = Math.Pow(z, 3) / (x * w) * h;
        }

        double k = SpeedFunction(speed);
        return y * k;

    }

    private static double SpeedFunction(double x)
    {
        //Giper parameters
        double h = 1; double w = 2;

        double y = ((-1 / Math.Pow((Math.Abs(x) * w + 1), 3)) + 1) * h;

        return y;

    }

    private static double AccelerationFunction(double speed)
    {
        // Giper parameters
        double p = 0.1;

        double x = speed + 1;

        x = Math.Pow(p / x, 2) - 1 / (Math.Pow(acceleration / p, 2));

        x++;

		double y = p / Math.Sqrt(x + (1 / Math.Pow(acceleration / p, 2)));

        return y;
    }

    private double FrictionFunction(double speed)
    {
        double speedDelta = 0;

        if (speed > friction)
        {
            speedDelta -= friction;
        }
        else if (speed < -friction)
        {
            speedDelta += friction;
        }
        else
        {
            speedDelta = -speed;
        }

        return speedDelta * ScaleFunction();
    }

    private Vector3 WheelsShift()
    {
        var shift = Math.Sqrt(0.5 * Size.x * (1 - Math.Cos(this.WheelAngle)));

        var shiftX = (float)(Math.Sin(shift) * Math.Cos(this.Direction) * this.TurnAngle * ValueSignDependency(this.Speed));
        var shiftY = -(float)(Math.Sin(shift) * Math.Sin(this.Direction) * this.TurnAngle * ValueSignDependency(this.Speed));

        Vector3 vector = new Vector3(shiftX,shiftY);

        return vector;
    }

    private double ScaleFunction()
    {
        return this.Size.x;
    }

    private static int ValueSignDependency(double value)
    {
        return value > 0 ? 1 : -1;
    }
}