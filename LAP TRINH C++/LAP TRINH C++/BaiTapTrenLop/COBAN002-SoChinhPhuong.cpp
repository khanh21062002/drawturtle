// #include<stdio.h>
// #include<iostream>
// #include<math.h>
// #include <bits/stdc++.h>

// using namespace std;

// int KiemTraSoChinhPhuong(long long int n){
//     long long int sqr = sqrt(n);
//     if (sqr*sqr==n){
//         return 1;
//     }
//     else{
//         return 0;
//     }
// }
// bool ChuSoChinhPhuong(long long int n){
//     bool cs =false;
//     while(n > 0)
//     {
//        int t = n % 10;
//         if(t == 0 || t == 1 || t == 4 || t == 9)
//            {
//                cs = true;
//            }
//         else{
//             cs = false;
//         }
//         n = n / 10;
//     }
//     if (cs == true){
//         return true;
//     }
//     else{
//         return false;
//     }
// }

// int main(){
//     int t;
//     cin >> t;
//     while (t--)
//     {
//         int k;
//         cin >> k;
//         int dem =0;
//         int temp = 0;
//         long long int n = pow(10,k)-1;
//         int a[n];
//         for ( int i = pow(10,k-1); i<= pow(10,k)-1; i++){
//             if(KiemTraSoChinhPhuong(i)==1 && ChuSoChinhPhuong(i) == true){
//                 a[dem++] = i;
//                 temp ++;
//             }
//         }
//         if (temp == 0){
//             cout << "-1\n";
//         }
//         else if(temp !=0 && k==1){
//             cout << "0\n";
//         }
//         else{
//             int min = a[0];
//             for( int i = 0 ; i < dem ; i++){
//                if(min > a[i]){
//                   a[i] = min;
//                 }
//             }
//         cout << min << "\n" ;
//         }
//     }
//     return 0;
// }

#include <bits/stdc++.h>

using namespace std;

bool KiemTraSoChinhPhuong(long long n){
	if(n == 0)	{
        return true;
    }
	int can = sqrt(n);
	if(can * can == n)	return true;
	return false;
}

long long Ktra(int k){
	long long dau = (long long)pow(10, k - 1);
	long long cuoi = (long long)pow(10, k) - 1;
	if(KiemTraSoChinhPhuong(dau)){
        dau = (long long)sqrt(dau);
    }
	else {
        dau = (long long)sqrt(dau) + 1;
    }
	while(dau * dau <= cuoi){
			bool ok = true;
			long long tmp = dau * dau;
			while(tmp){
				if(!KiemTraSoChinhPhuong(tmp % 10)){
					ok = false;
					break;
				}
				tmp /= 10;
			}
			if(ok)	return (dau * dau); 
			else	dau = dau + 1;
	}
	return -1;
}

int main(int argc, char const *argv[])
{
    int t;	cin >> t;
    while(t--){
    	int k;	cin >> k;
    	if(k < 1 || k > 18)	cout << "-1";
    	else if(k == 1)		cout << "1";
    	else if(k % 2 == 1)	cout << (long long)pow(10, k - 1);			
    	else				cout << Ktra(k);
    	cout << endl;
    }
    return 0;
}