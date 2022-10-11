#include<stdio.h>
#include<iostream>
#include<string.h>

using namespace std;


int main(){
    int t;
    cin >> t;
    while (t--)
    {
        char n[500];
        cin >> n;
        bool test;
        for( int i =0; i < strlen(n); i++){
            if((n[i] == '2' || n[i] == '3' || n[i] == '5' || n[i] == '7') && (n[i] == n[strlen(n) - 1 - i]) ){
                test = true;
            }
            else if ((n[i] != '1' || n[i] != '4' || n[i] != '6'||n[i] != '8' || n[i] != '9') && (n[i] != n[strlen(n) - 1 - i]))
            {
                test == false;
            }
            else{
                test == false;
            } 
        }
        // cout << strlen(n);
        if ( test ==  true){
            cout << "YES\n";
        }
        else{
            cout << "NO\n";
        }
    }
    return 0;
}