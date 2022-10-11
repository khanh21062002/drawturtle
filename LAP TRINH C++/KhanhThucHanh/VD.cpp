#include <iostream>
using namespace std;
int main()
{
   int num,copy_of_num,sum=0,rem;
   cout<<endl<<"Nhập vào một số: ";
   cin>>num;
   copy_of_num = num;
   while (num != 0)
   {
      rem = num % 10;
      sum = sum + (rem*rem*rem);
      num = num / 10;
   }
   if(copy_of_num == sum)
      cout<<copy_of_num<<" là số Armstrong";
   else
      cout<<copy_of_num<<" không phải là số Armstrong";
   cout<<"\n-------------------------\n";
   cout<<"Chương trình này được đăng tại Freetuts.net";
}