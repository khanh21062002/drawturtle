#include<stdio.h>
#include<iostream>
#include<bits/stdc++.h>

using namespace std;
void Count(int a[], int n){
    int b[n];
    int x=1;
    b[0]=a[0];
    //tach mang chi gom cac phan tu khac nhau
    for(int i=1;i<n;i++){
        int dem=0;
        for(int j=0;j<x;j++){
            if(a[i]==b[j])
                dem++;
        }
        if(dem==0){
            b[x]=a[i];
            x++;
        }
    }
    //tao mang chua so lan xuat hien cua moi phan tu
    int c[x];
    for(int i=0;i<x;i++)
        c[i]=0;
    //dem so lan xuat hien cua moi phan tu
    for(int i=0;i<x;i++){
        for(int j=0;j<n;j++){
            if(a[j]==b[i])
                c[i]++;
        }
    }
     
    //tim so lan xuat hien nhieu nhat
    //bien y dung de dem so lan xuat hien bang nhau
    int max=c[0], vtri=0, y=1;
     
    //tim so lan xuat hien nhieu nhat va vi tri cua no
    for(int i=1;i<x;i++){
        if(c[i]>max){
            max=c[i];
            vtri=i;
            y=1;
        }
        if(c[i]==max){
            y++;
        }       
    }
     
    //neu chi co mot phan tu xuat hien nhieu nhat
    if(y==1){
        cout<<b[vtri]<<"\n";
        /* cout<<" xuat hien "<<max<<" lan"<<endl; */
    }
     
    //neu co tu hai phan tu xuat hien nhieu nhat
    else{
        int d[y]; //mang dung luu vi tri cua phan tu xuat hien nhieu nhat
        int z=0;
        for(int i=0;i<x;i++)
            if(c[i]==max){//lay vi tri cua phan tu xuat hien nhieu nhat
                d[z]=i;
                z++;
            }
        //in ket qua ra man hinh
        //cout<<"\nPhan tu xuat hien nhieu nhat la: ";
        for(int i=0;i<z;i++ )
            {cout<<b[d[i]]<<" ";}
        cout << "\n";
        //cout<<" xuat hien "<<max<<" lan";
         
    }
}

int main(){
    int t;
    cin >> t;
    while (t--)
    {
        int n;
        cin >> n;
        int a[100];
        for( int i = 0 ; i < n; i++){
            cin >> a[i];
        }
        Count(a,n);
    }
    return 0;
}