#include<bits/stdc++.h>
#include<iostream>
#include<math.h>
using namespace std;

long long Count(long long n){
    long long count = 0;
    while (n > 0)
    {
        n/=10;
        count++;
    }
    return count;
}

int main()
{
   int t;
   cin >> t;
   while (t--)
   {
      long long n,copy_of_num,tong=0,k,rem;
      cin>>n;
      copy_of_num = n;
      k = Count(n);
      while (n > 0)
      {
        rem = n % 10;
        tong = tong + pow(rem,k);
        n = n / 10;
      }
      if(copy_of_num == tong)
        {
            cout<< "1" << endl;
        }
      else
      {
        cout<< "0" << endl ;
      } 
   }
}