#include<stdio.h>
#include<iostream>
#include<string.h>

using namespace std;

int lmax ;
int lmin ;

void TimLen( char str1[], char str2[]){
    lmax = strlen(str1);
    lmin = strlen(str2);
    if ( lmax < lmin){
        int tg = lmax;
        lmax = lmin;
        lmin = tg;
    }
}

// void SoSanh(char str1[], char str2[]){
//     if(strcmp(str1,str2) < 0){ 
//     }
// }

void Noi( char str1[], char str2[]){
    strrev(str1);
    strrev(str2);
    if(strlen(str1) == lmax){
        strcat(str1,"0");
        for ( int i =0 ; i <= lmax - lmin ; i++){
            strcat(str2,"0");
        }
    }
    else{
        strcat(str2,"0");
        for(int i =0; i <= lmax - lmin;i++){
            strcat(str1,"0");
        }
    }
}


int main(){
    int t;
    cin >> t;
    while (t--)
    {
        char a[1000];
        char b[1000];
        char s[1000];
        cin >> a;
        cin >> b;

    }
    
}