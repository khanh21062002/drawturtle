#include<stdio.h>
#include<iostream>

using namespace std;

void swap1(int a[], int n)
{
	for (int i = 0;i < n - 1;i++)
	{
		for (int j = i + 1;j < n;j++)
		{
			if (a[i] > a[j])
			{
				int swap1 = a[i];
				a[i] = a[j];
				a[j] = swap1;
			}
		}
	}
}

int main(){
    int n;
    int a[100],c[100];
    cin >> n;
    int dem =0;
    for (int i=0; i < n; i++){
        cin >> a[i];
    }
    int index, value;
    for (int i =0;  i <n ; i++){
        index = i;
		value = a[i];
		while (index > 0 && a[index - 1] > value)
		{
			a[index] = a[index - 1];
			index--;
		}
		a[index] = value;
		c[dem++] = value;
        swap1(c, dem);
        cout << "Buoc " << dem -1 << " : ";
        for( int j =0; j <dem ; j++){
            cout << c[j] << " ";
        }
        cout << "\n";
    }
    return 0;
}