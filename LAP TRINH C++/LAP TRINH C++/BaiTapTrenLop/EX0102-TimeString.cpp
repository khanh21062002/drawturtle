#include<stdio.h>
#include<iostream>
#include<bits/stdc++.h>

using namespace std;

int main(){
    int t;
    cin >> t;
    while (t--)
    {
        char time[5];
        char time_out[5];
        cin >> time;
        if(time[0] == '?'){
            if( time[1] == '0' || time[1] == '1' || time[1] == '2' || time[1] == '3'||time[1] == '?' ){
                time[0] = '2' ;
            }
            else{
                time[0] = '1';
            }
        }
        if(time[1] == '?'){
            if(time[0] == '1' || time[0] == '0'){
                time[1] = '9';
            }
            else if(time[0] == '2'||time[0] == '?'){
                time[1] = '3';
            }
            else{
                time[1] = '3';
            }
        }
         if(time[0] == '?' && time[1] == '?'){
             time[0] = '2';
             time[1] = '3';
         }
        if(time[3] == '?'){
            if( '0' <= time[4] && time[4] <= '9'){
                time[3] = '5';
            }
        }
        if(time[4] == '?'){
            if( '0' <=time[3] && time[3] <= '5'){
                time[4] = '9';
            }
        }
        if(time[3] == '?' && time[4] == '?' ){
            time[3] = '5';
            time[4] = '9';
        }
        cout << time << endl;
    }
}