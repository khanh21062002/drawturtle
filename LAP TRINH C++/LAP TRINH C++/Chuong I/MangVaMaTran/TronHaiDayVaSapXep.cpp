#include<stdio.h>
#include<iostream>
#include<bits/stdc++.h>

using namespace std;

void NhapMang(int a[],int b[], int n){
    for(int i = 0; i < n; i++){
        cin >> a[i];
    }
    for (int j = 0; j < n ; j++){
        cin >> b[j];
    }
}

void TangDan(int a[], int n){
    int tg;
    for(int i = 0; i < n - 1; i++){
        for(int j = i + 1; j < n; j++){
            if(a[i] > a[j]){
                // Hoan vi 2 so a[i] va a[j]
                tg = a[i];
                a[i] = a[j];
                a[j] = tg;        
            }
        }
    }
}

void GiamDan(int a[], int n){
    int tg;
    for(int i = 0; i < n - 1; i++){
        for(int j = i + 1; j < n; j++){
            if(a[i] < a[j]){
                // Hoan vi 2 so a[i] va a[j]
                tg = a[i];
                a[i] = a[j];
                a[j] = tg;        
            }
        }
    }
}

void GhepMang(int a[], int na,int b[], int nb, int c[], int &nc){
    int ia=0, ib=0;
    nc =0;
    while (ia<na && ib<nb)
    {
        c[nc++] = a[ia++];
        c[nc++] = b[ib++];
    }
    while (ia<na)
    {
        c[nc++] = a[ia++];
    }
    while (ib<nb)
    {
        c[nc++] == b[ib++];
    }
    cout << c[nc];
}

int main(){
    int t;
    cin >> t;
    while (t--)
    {
        int n;
        cin >> n;
        int a[100], b[100],c[100];
        NhapMang(a,b,n);
        TangDan(a,n);
        GiamDan(b,n);
        GhepMang(a,n,b,n,c,n);
    }
    return 0;
}