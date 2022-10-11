#include<iostream>
#include<stdio.h>

using namespace std;


class nhan
{
	public:
	char X[2000110];
	void XL(long n);
};
void nhan::XL(long n)
{
	long k=2000100;
	long t=0;
    X[k--]=0;
	for(long i=1;i<n;i++)
	{
		t=t+i;
		X[k--]=t%10+'0';
		t=t/10;
	}
	for(long i=n;i>=1;i--)
	{
		t=t+i;
		X[k--]=t%10+'0';
		t=t/10;
	}
	while(t>0)
	{
		X[k--]=t%10+'0';
		t=t/10;
	}
    k++;
	printf("%s",X+k);
}
class Test
{
	int n;
	long *A;
	public:
	void xl();
};

void Test::xl()
{
	cin>>n;
	nhan M;
	A=new long [n+5];
	for(int i=1;i<=n;i++) cin>>A[i];
	for(int i=1;i<=n;i++) 
	{
		M.XL(A[i]);
		cout<<"\n";
	}	
}


int main()
{
	Test T;
	T.xl();
}


