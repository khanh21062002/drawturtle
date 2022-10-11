#include<stdio.h>
#include<iostream>

using namespace std;


int main(){
    int ga, cho;
    for( cho = 1 ; cho < 36 ; cho++){
        for(ga = 35; ga > 0 ; ga--){
            if(ga + cho == 36 && 2*ga + 4*cho == 100){
                cout << ga  << "\n";
                cout << cho <<  "\n";
        }
    }
  }
}