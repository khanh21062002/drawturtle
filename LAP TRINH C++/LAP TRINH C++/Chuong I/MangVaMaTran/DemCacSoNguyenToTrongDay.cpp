#include<stdio.h>
#include<iostream>
#include<math.h>
#include<bits/stdc++.h>

using namespace std;

void NhapMang(int a[], int n){
    for(int i = 0; i < n; i++){
        cin >> a[i];
    }
}
void XuatMang(int a[], int n){
    for(int i = 0;i < n; i++){
        cout << a[i];
    }
}
bool isSoNguyenTo(long int n){
    if (n<2){
        return false;
    }
    else{
      int count = 0;
      for (int i=2 ; i<=sqrt(n); i++){
         if (n % i == 0){
             count++;
         }
      }
      if (count ==0){
        return true;
      }
      else{
        return false;
      }
    }
}
/* void Count(int a[], int n){
    int b[n];
    int x=1;
    b[0]=a[0];
    //tach mang chi gom cac phan tu khac nhau
    for(int i=0 ;i<n;i++){
        if(isSoNguyenTo(a[i]))
        {
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

} */

int main(){
    int t;
    cin >> t;
    for (int k = 1; k <= t; k++)
    {
        int n;
        int a[100],b[100];
        int dem =0;
        cin >> n;
        NhapMang(a,n);
        cout << "Test " << k <<": \n";
        for (int i = 0;i < n;i++)
		{
			if(isSoNguyenTo(a[i]))
            {
                b[dem++] = a[i];
            }
		}
        for (int i = 0; i < dem; i++)
        {
            for (int j = i + 1;j < dem;j++)
			{
				if (b[i] > b[j])
				{
					int temp = b[j];
					b[j] = b[i];
					b[i] = temp;
				}
			}
        }
        for (int i = 0; i < dem; i++) 
		{
			int dem1 = 0;
			int count = 1;
			for (int j = 0; j < dem; j++) 
			{
				if (i == j)
					continue;
				if (b[i] == b[j] && i > j)
					dem1 = 1;
				if (b[i] == b[j]) 
				{
					count++;
				}
			}
			if (dem1 == 0) 
			{
				cout << b[i] << " xuat hien "<< count << " lan \n";
			}
		}
		printf("\n");
    }
    return 0; 
}