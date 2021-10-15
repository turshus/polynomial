using System;

class Program {
    static void Main(string[] args) {
        Console.WriteLine("Starting Polynomial Problem");
        double[] poly1 = {2, 5, 4, 1};
        double[] poly2 = {3, 1, 2, 1};

        double[] productPoly = polyM(poly1, poly2);

        for(int i = productPoly.Length; i > 0; i--) {
            Console.Write("{0}x^{1}  ", productPoly[i - 1], i - 1);
        }
    }

    static double[] polyM(double[] a_poly1, double[] a_poly2) {
        //allocate memory for our array
        double[] l_polyProduct = new double[a_poly1.Length + a_poly2.Length - 1];

        for(int i = 0; i < a_poly1.Length; i++) {
            for(int j = 0; j < a_poly2.Length; j++) {
                l_polyProduct[i + j] += a_poly1[i] * a_poly2[j];
            }
        }

        return l_polyProduct;
    }
}