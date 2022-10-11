#include<stdio.h>
#include<iostream>
#include<math.h>

using namespace std;

int isSoNguyenTo(long int n){
    int count = 0;
    for (int i=2 ; i<=sqrt(n); i++){
        if (n % i == 0){
            count++;
        }
    }
    if (count ==0){
        return 1;
    }
    else{
        return 0;
    }
}
bool ktra(long int x){
    int tong = 0;
    bool cs = true;
    while(x > 0)
    {
       int t = x % 10;
        if(t != 2 && t != 3 && t != 5 && t != 7)
           {
               cs = false;
           }
        tong = tong + t;
        x = x / 10;
    }
 if(isSoNguyenTo(tong) && cs == true)
  return true;
 return false;
 }
int main(){
    int t;
    cin >> t;
    while (t--)
    {
        long int a,b,i;
        cin >> a;
        cin >> b;
        int dem =0;
        if (a<b){
            for(i =a ; i <= b; i++){
                if(isSoNguyenTo(i)&&ktra(i)){
                    dem++;
                }
            }
            cout << dem << "\n";
        }
        else{
            for(i =b ; i <= a; i++){
                if(isSoNguyenTo(i)&&ktra(i)){
                    dem++;
                }
            }
            cout << dem << "\n";
        }
    }
    return 0;
}