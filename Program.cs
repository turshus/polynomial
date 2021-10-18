using System;
class Program {
    static void Main(string[] args) {
        while(true) {
            //check if user wants to run empirical study or simple problem
            Console.Write("E : Empirical Study\nS : Simple Problem\nQ : Quit\nYour choice: ");
            string l_usrChoice = Console.ReadLine().ToLower();
            if(string.Equals(l_usrChoice, "e")) {
                //if a single problem has taken more than 2 hours, time to quit.
                DateTime l_empiricalStart = DateTime.Now;
                TimeSpan l_schoolBookTotTime = new TimeSpan();
                TimeSpan l_4subPolyTotTime = new TimeSpan();
                TimeSpan l_3subPolyTotTime = new TimeSpan();

                //loop from n = 32 to as large as possible
                int i = 5;
                while(true) {
                    //check if more than 2 hours has elapsed.  Quit if it has
                    if((DateTime.Now - l_empiricalStart).TotalMinutes > 120) {
                        Console.WriteLine("\n\nFinishing Empirical Study...");
                        Console.WriteLine("School Book Total Time: {0}", l_schoolBookTotTime);
                        Console.WriteLine("4 Sub Poly Total Time:  {0}", l_4subPolyTotTime);
                        Console.WriteLine("3 Sub Poly Total Time:  {0}", l_3subPolyTotTime);
                        Console.WriteLine("Total time: {0}\n\n", (DateTime.Now - l_empiricalStart).ToString());
                        break;
                    }

                    Console.WriteLine("n = {0}", Math.Pow(2, i));

                    //generate 10 arrays that will be multiplied
                    double[][] l_pArrays = new double[10][];
                    double[][] l_qArrays = new double[10][];
                    for(int j = 0; j < 10; j++) {
                        l_pArrays[j] = arrayGen(i);
                        l_qArrays[j] = arrayGen(i);
                    }

                    //run the 10 problems
                    //generate the starting time
                    DateTime l_schoolBookStart = DateTime.Now;
                    //school-book
                    for(int j = 0; j < 10; j++) {
                        double[] l_schoolBookPoly = schoolBookPoly(l_pArrays[j], l_qArrays[j]);
                    } 
                    TimeSpan l_schoolBookTime = DateTime.Now - l_schoolBookStart;
                    l_schoolBookTotTime.Add(l_schoolBookTime);
                    Console.WriteLine("School Book Time:  {0}", l_schoolBookTime.ToString());

                    //4 sub problem
                    DateTime l_4SubStart = DateTime.Now;
                    for(int j = 0; j < 10; j++) {
                        double[] l_4subPoly = fourSubPoly(l_pArrays[j], l_qArrays[j]);
                    }
                    TimeSpan l_4SubTime = DateTime.Now - l_4SubStart;
                    l_4subPolyTotTime.Add(l_4SubTime);
                    Console.WriteLine("4 Sub Poly Time:  {0}", l_4SubTime.ToString());

                    //3 sub problem
                    DateTime l_3SubStart = DateTime.Now;
                    for(int j = 0; j < 10; j++) {
                        double[] l_3subPoly = threeSubPoly(l_pArrays[j], l_qArrays[j]);
                    }
                    TimeSpan l_3SubTime = DateTime.Now - l_3SubStart;
                    l_3subPolyTotTime.Add(l_3SubTime);
                    Console.WriteLine("3 Sub Poly Time: {0}\n\n", l_3SubTime.ToString());

                    i++;
                }
            } else if(string.Equals(l_usrChoice, "s")) {
                //Simple Problem.  Should return (x^6 + 6x^5 + 14x^4 + 19x^3 + 21x^2 + 17x + 6)
                double[] l_polyP = new double[] { 2, 5, 4, 1 };
                double[] l_polyQ = new double[] { 3, 1, 2, 1 };

                //schoolbook
                double[] l_schoolBook = schoolBookPoly(l_polyP, l_polyQ);
                Console.Write("\nSchool-book:\n  ");
                for(int i = l_schoolBook.Length; i > 0; i--) {
                    Console.Write("{0}x^{1}  ", l_schoolBook[i - 1], i - 1);
                }

                //four-sub (D&C)
                double[] l_fourSubDC = fourSubPoly(l_polyP, l_polyQ);
                Console.Write("\n\nFour Sub:\n  ");
                for(int i = l_fourSubDC.Length; i > 0; i--) {
                    Console.Write("{0}x^{1}  ", l_fourSubDC[i - 1], i - 1);
                }

                //three-sub (D&C)
                double[] l_threeSubDC = threeSubPoly(l_polyP, l_polyQ);
                Console.Write("\n\nThree Sub:\n  ");
                for(int i = l_threeSubDC.Length; i > 0; i--) {
                    Console.Write("{0}x^{1}  ", l_threeSubDC[i - 1], i - 1);
                }

            } else if(string.Equals(l_usrChoice, "q")) {
                Console.WriteLine("\nGoodbye\n");
                break;
            } else {
                Console.WriteLine("\nError: Sorry, I didn't understand your input...\nEnter either a 'e' or 's'\n");
            }
            Console.WriteLine("\n\n## Finished ##");
            Console.WriteLine("##############\n\n");
        }  
    }

    static double[] schoolBookPoly(double[] a_poly1, double[] a_poly2) {
        if(a_poly1.Length <= 0 || a_poly2.Length <= 0) {
            return new double[0];
        }
        //allocate memory for our array
        double[] l_polyProduct = new double[a_poly1.Length + a_poly2.Length - 1];

        for(int i = 0; i < a_poly1.Length; i++) {
            for(int j = 0; j < a_poly2.Length; j++) {
                l_polyProduct[i + j] += a_poly1[i] * a_poly2[j];
            }
        }
        return l_polyProduct;
    }

    static double[] fourSubPoly(double[] a_polyP, double[] a_polyQ) {
        //base cases
        if(a_polyP.Length == 1 && a_polyQ.Length == 1) {
            return new double[] { a_polyP[0] * a_polyQ[0] };
        }

        //splice P & Q into pHigh, pLow, qHigh, qLow
        double[] l_pHigh = new double[a_polyP.Length / 2];
        double[] l_pLow  = new double[a_polyP.Length / 2];
        double[] l_qHigh = new double[a_polyQ.Length / 2];
        double[] l_qLow  = new double[a_polyQ.Length / 2];

        //fill pHigh and pLow
        for(int i = 0; i < (a_polyP.Length / 2); i++) {
            l_pLow[i] = a_polyP[i];
            l_pHigh[i] = a_polyP[(a_polyP.Length / 2) + i];
        }

        //fill qHigh and qLow
        for(int i = 0; i < (a_polyQ.Length / 2); i++) {
            l_qLow[i] = a_polyQ[i];
            l_qHigh[i] = a_polyQ[(a_polyQ.Length / 2) + i];
        }

        //run the problem with the 4 smaller problems
        double[] l_front = fourSubPoly(l_pLow, l_qLow); // placed at the end
        double[] l_middle1 = fourSubPoly(l_pHigh, l_qLow); // added to n/2
        double[] l_middle2 = fourSubPoly(l_pLow, l_qHigh); // added to n/2
        double[] l_back = fourSubPoly(l_pHigh, l_qHigh); //placed at the front

        //piecewise add the middles
        double[] l_middle = new double[l_middle1.Length];
        for(int i = 0; i < l_middle.Length; i++) {
            l_middle[i] = l_middle1[i] + l_middle2[i];
        }

        //allocate memory for return array
        double[] l_solution = new double[a_polyP.Length + a_polyQ.Length - 1];

        for(int i = 0; i < l_front.Length; i++) {
            l_solution[i] += l_front[i];
        }
        for(int i = 0; i < l_middle.Length; i++) {
            l_solution[((l_solution.Length + 1) / 4) + i] += l_middle[i];
        }
        for(int i = 0; i < l_back.Length; i++) {
            l_solution[(l_solution.Length - l_back.Length) + i] += l_back[i];
        }
        return l_solution;
    }

    static double[] threeSubPoly(double[] a_polyP, double[] a_polyQ) {
        //base case(s) --> make sure the poly returned is 2* the input length!
        if(a_polyP.Length == 1 && a_polyQ.Length == 1) {
            return new double[] { a_polyP[0] * a_polyQ[0] };
        }

        //splice P and Q into Phigh, Plow, Qhigh, Qlow
        double[] l_pHigh = new double[a_polyP.Length / 2];
        double[] l_pLow  = new double[a_polyP.Length / 2];
        double[] l_qHigh = new double[a_polyQ.Length / 2];
        double[] l_qLow  = new double[a_polyQ.Length / 2];

        //fill pHigh and pLow
        for(int i = 0; i < (a_polyP.Length / 2); i++) {
            l_pLow[i] = a_polyP[i];
            l_pHigh[i] = a_polyP[(a_polyP.Length / 2) + i];
        }

        //fill qHigh and qLow
        for(int i = 0; i < (a_polyQ.Length / 2); i++) {
            l_qLow[i] = a_polyQ[i];
            l_qHigh[i] = a_polyQ[(a_polyQ.Length / 2) + i];
        }
        
        //peicewise add Phigh and Plow, same with Qhigh Qlow (use numpy to make the code simple)
        double[] l_pPiecewise = new double[l_pHigh.Length];
        double[] l_qPiecewise = new double[l_qHigh.Length];

        for(int i = 0; i < l_pHigh.Length; i++) {
            l_pPiecewise[i] = l_pHigh[i] + l_pLow[i];
        }

        for(int i = 0; i < l_qHigh.Length; i++) {
            l_qPiecewise[i] = l_qHigh[i] + l_qLow[i];
        }

        //call PolyMult three times, (Plow, Qlow), (Phigh, Qhigh) and the P and Q high, low piecewise added above
        double[] front = threeSubPoly(l_pLow, l_qLow); //starts at x^0 and goes up
        double[] middle = threeSubPoly(l_pPiecewise, l_qPiecewise); 
        double[] back = threeSubPoly(l_pHigh, l_qHigh);//starts at x^n-1 and goes down

        //calculate the middle solution by subtracting the solutions as described in class
        for(int i = 0; i < middle.Length; i++) {
            middle[i] -= back[i];
            middle[i] -= front[i];
        }

        //allocate memory for the solution of length 2*len(P)
        double[] l_solution = new double[a_polyP.Length + a_polyQ.Length - 1];

        //accumulate the terms based on the powers of x into the solution, return it
        for(int i = 0; i < front.Length; i++) {
            l_solution[i] += front[i];
        }
        for(int i = 0; i < middle.Length; i++) {
            l_solution[((l_solution.Length + 1) / 4) + i] += middle[i];
        }
        for(int i = 0; i < back.Length; i++) {
            l_solution[(l_solution.Length - back.Length) + i] += back[i];
        }

        //returns array of size 2 * poly1.Length
        return l_solution;
    }

    static double[] arrayGen(int a_problemSize) {
        if(a_problemSize <= 0) {
            return new double[]{};
        }
        //all problems need to be a power of 2
        double[] l_newProblem = new double[Convert.ToInt64(Math.Pow(2, a_problemSize))];

        //Generate a list of random Doubles between 0.0 < x < 10.0
        for(int i = 0; i < l_newProblem.Length; i++) {
            //add in values between 0 - 9 with 2 decimal spots
            l_newProblem[i] = new Random().Next(-1, 1) + Convert.ToDouble(new Random().NextDouble().ToString("f2"));
        }

        return l_newProblem;
    }
}