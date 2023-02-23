#include <bits/stdc++.h>
#define MAX 100
#define TRUE 1
#define FALSE 0
using namespace std;
int n, X[MAX], OK=TRUE, dem=0;
void Init (void ){
cout<<"\n Nhap n=";cin>>n;
for (int i=1; i<=n; i++)
X[i] =0;
}
void Result(void){
cout<<"\n Ket qua "<<++dem<<":";
for (int i=1; i<=n; i++)
cout<<X[i]<<" ";
}
void Next_Bit_String(void) {
int i= n;
while (i>0 && X[i]!=0){
X[i] = 0; i --;
}
if (i > 0 ) X[i] = 1;
else OK = FALSE;
}
int main() {
Init(); //Nhap n = 4
while (OK ){
Result();
Next_Bit_String();
}
system("PAUSE");
return 0;
}
