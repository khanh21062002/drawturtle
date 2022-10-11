#include<bits/stdc++.h>
#include<math.h>

using namespace std;

int main(){
    int num ;
    cout << "PLEASE ENTER a TV channel number : ";
    cin >> num ;
    switch (num)
    {
    case 7 :
        cout << "The call letters for this channel is: VTV1 \n";
        break;
    case 9 :
        cout << "The call letters for this channel is: VTV2 \n";
        break;
    case 14:
        cout << "The call letters for this channel is: VTV3 \n";
        break;
    case 19:
        cout << "The call letters for this channel is: ESPN \n";
        break;
    case 25:
        cout << "The call letters for this channel is: VCTV1 \n";
        break;
    case 31:
        cout << "The call letters for this channel is: HANOI1 \n";
        break;
    case 44:
        cout << "The call letters for this channel is: HTV1 \n";
        break;
    case 63:
        cout << "The call letters for this channel is: HTV2 \n";
        break;
    default:
        cout << "There are no channels under this letter \n";
        break;
    }
    return 0;
}