#include<stdio.h>
#include<iostream>
#include<math.h>

using namespace std;
int Fibonacci(int n)
{
    if (n == 1 || n == 2)
        return 1;
    return Fibonacci(n - 1) + Fibonacci(n - 2);
}

int main(){
    int t;
    cin >> t;
    while (t--)
    {
        int n;
        cin >> n;
        Fibonacci(n);
        cout << Fibonacci(n)<< "\n";  
    }
    return 0;
}