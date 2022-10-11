#include"statistics.h"
#include<math.h>
double mean(double a)
{
    return a;
}
double mean(double a, double b){
    return (a+b)/2;
}
double mean(double a, double b, double c){
    return (a+b+c)/3;
}
double mean(double a, double b, double c, double d){
    return (a+b+c+d)/4;
}

double variance(int a){
    double variance = 0;
    double stdDeviation = 0;
    variance = pow(a - mean(a), 2);
    variance = variance/1;
    stdDeviation = sqrt(variance);
    return stdDeviation;
}
double variance(int a , int b){
    double variance = 0;
    double stdDeviation = 0;
    variance = pow(a - mean(a,b), 2) + pow(b - mean(a,b), 2);
    variance = variance / 2;
    stdDeviation = sqrt(variance);
    return stdDeviation;
}
double variance(int a , int b, int c){
    double variance = 0;
    double stdDeviation = 0;
    variance = pow(a - mean(a,b,c), 2) + pow(b - mean(a,b,c), 2) + pow(c - mean(a,b,c), 2) ;
    variance = variance / 3;
    stdDeviation = sqrt(variance);
    return stdDeviation;
}
double variance(int a , int b, int c , int d){
    double variance = 0;
    double stdDeviation = 0;
    variance = pow(a - mean(a,b,c,d), 2) + pow(b - mean(a,b,c,d), 2) + pow(c - mean(a,b,c,d), 2) + pow(d - mean(a,b,c,d), 2) ;
    variance = variance / 4;
    stdDeviation = sqrt(variance);
    return stdDeviation;
}