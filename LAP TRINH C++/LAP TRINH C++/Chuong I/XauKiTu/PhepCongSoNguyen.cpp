#include<bits/stdc++.h>
#include<string.h>
#include<iostream>

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

void cong( char str1[], char str2[], char s[]){
    int nho =0,t;
    for (int i =0 ; i<=lmax; i++){
        t = str1[i] - '0' + str2[i] - '0' + nho;
        s[i] = t%10 + '0';
        nho = t/10;
    }
    if(s[lmax] == '0'){
        s[lmax] = '\0';
    }
    else{
        s[lmax+1] = '\0';
    }
    strrev(s);
    cout << s << '\n';
}

int main(){
    int t;
    cin >> t;
    while (t--)
    {
        char a[255];
        char b[255];
        char s[255];
        cin >> a;
        cin >> b;
        TimLen(a,b);
        Noi(a,b);
        cong(a,b,s);
    }
    return 0;
}