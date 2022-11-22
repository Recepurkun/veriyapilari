using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace veriproje
{
    internal class Program
    {
        static void Main(string[] args)
        {
            singlylinkedlist sgl = new singlylinkedlist();
            sgl.insert("Benim Ikilim Menü", "2 Adet Tavuk Burger", "Patates Kizartmasi(Orta)", 75, 0);
            sgl.insert("Whopper Menü", "Whopper", "Patates Kizartmasi(Kova)", 95, 1);
            sgl.insert("Ozel Menü", "Double Whopper Jr ve Double Kofteburger", "Patates Kizartmasi(Buyuk)", 110, 2);
            foreach (var item in sgl)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("Sirada toplam bu kadar siparis var: " + (sgl.size()) + "\n" );

            Console.WriteLine("______________________Durum 2______________________\n");

            sgl.insert("Araya", "deger", "eklemek", 333, 2);
            //sgl.insert("deneme", "deneme", "deneme", 666, 4);
            foreach (var item in sgl)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("Sirada toplam bu kadar siparis var: " + (sgl.size()) + "\n");

            Console.WriteLine("______________________Durum 3______________________\n");
            sgl.Find("Whopper Menü");
            
            /* burayi aktif ettiğimizde butun kuyrugu temizleyecegi icin durum 6'da hata verecek var olmayan bir pozisyonu gonderdigimiz icin
            Console.WriteLine("______________________Durum 4______________________\n");
            sgl.clear();
            Console.WriteLine("Sirada toplam bu kadar siparis var: " + (sgl.size()));*/

            Console.WriteLine("______________________Durum 5______________________\n");
            Console.WriteLine("Sira da siparis kaldi mi?: " + sgl.isFull());

            Console.WriteLine("______________________Durum 6______________________\n");
            sgl.delete(1);//olmayan bir pozisyon numarasi girersek hata verecek
            Console.WriteLine("Silinme islemi gerceklestirildi.\nGeride kalan siparisler:\n ");
            foreach (var item in sgl)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("______________________Durum 7______________________\n");
            //1.indekse sahip kayit silindikten sonra böyle bir kayit var mi diye aramaya calisiyoruz.
            sgl.Find("Whopper Menü");
            Console.ReadLine();
        }
    }
    class singlylinkedlist : IEnumerable // ana classimiz 
    {
        private class Node // Node classimiz 
        {
            //bu node dediğimiz eleman bir sonraki elemanin referansını işaret eder bu şekilde bağlama yapmıs oluyoruz.
            public string menu_adi;
            public string sandvic;
            public string patates_secimi;
            public double fiyat;
            public Node next = null;
            public Node(string menu_adi, string sandvic, string patates_secimi, double fiyat)//yapici metodumuz | disaridan gelen verileri yapici metot ile degiskenlerimize atiyoruz
            {
                this.menu_adi = menu_adi;
                this.sandvic = sandvic;
                this.patates_secimi = patates_secimi;
                this.fiyat = fiyat;
            }
        }
        private Node First = null;
        private int count = 0;
        public singlylinkedlist() {}
        public singlylinkedlist(string menu_adi, string sandvic, string patates_secimi, double fiyat)
        {
            add(menu_adi, sandvic, patates_secimi, fiyat);
        }

        public void add(string menu_adi, string sandvic, string patates_secimi, double fiyat)
        {
            insert(menu_adi, sandvic, patates_secimi, fiyat, size());
        }

        public void insert(string menu_adi, string sandvic, string patates_secimi, double fiyat, int position)
        {
            if(count < position || position < 0) // count'umuz girdiğimiz pozisyon değerinden kücükse veya pozisyon degeri 0'dan kücükse hata firlat.
            {
                throw new Exception("Yanlis pozisyon girdiniz.");
            }
            /* ------------------------------------------------------------------------------------------------------  */
            Node temp = First;// temp diye gecici bir degisken tanimladik ve first'i ona atadık 
            Node prevNode = null;
            for (int i = 0; i < position; ++i, temp = temp.next)
            {
                prevNode = temp;
            }
            Node node = new Node(menu_adi, sandvic, patates_secimi, fiyat);
            if (First != temp) // listemizde herhangi bir veri var mi diye kontrol ediyor
            {
                if (temp != null && prevNode != null)//araya ekleme, temp degiskenimiz null degilse ve prevNode'umuz bir onceki node'u gosteren degerimiz null degilse gerceklestir
                {
                    prevNode.next = node;
                    node.next = temp;
                }
                else if (prevNode != null)//sona ekle
                {
                    prevNode.next = node;
                }
            }
            else // başa ekleme
            {
                node.next = First;
                First = node;
            }
            ++count;
        }

        public void delete(int position)
        {
            if (count < position || position < 0) // count'umuz girdiğimiz pozisyon değerinden kücükse veya pozisyon degeri 0'dan kücükse hata firlat.
            {
                throw new Exception("Yanlis pozisyon girdiniz.");
            }
            /* ------------------------------------------------------------------------------------------------------  */
            Node temp = First;
            Node prevNode = null;
            for (int i = 0; i < position; ++i, temp = temp.next)
            {
                prevNode = temp;
            }
            if(First != temp)//aradaki veya sondaki düğümü bağdan çıkar
            {
                if (prevNode != null)
                    prevNode.next = temp.next;
            }
            else//ilk düğümü bağlı listeden çıkar
            {
                prevNode = First;
                First = First.next;
                prevNode.next = null;
            }
            temp.next = null;
            --count;
        }

        public void Find(string menu_adi)//aramak icin gerekli method
        {
            int position = 0;
            bool var_Mi = false;
            Node temp;
            for (temp = First; temp != null; temp = temp.next)
            {
                ++position;
                if (temp.menu_adi == menu_adi)
                {
                    var_Mi = true;
                    break;
                }
            }
            if (var_Mi == true)
            {
                Console.WriteLine("Aradiginiz Menü: " + temp.menu_adi);
                Console.WriteLine("Sandvic: " + temp.sandvic);
                Console.WriteLine("Patates secimi: " + temp.patates_secimi);
                Console.WriteLine("Fiyati: " + temp.fiyat + "\n");
            }
            else
            {
                Console.WriteLine("Boyle bir kayit yoktur.");
            }
        }

        public int size()//boyutu kontrol ediyor 
        {
            return count;
        }

        public void clear()//temizliyor
        {
            First = null;
            count = 0;
        }

        public bool isFull()//boyutunu kontrol ediyor
        {
            if (count == 0)
            {
                return false;
            }
            else
            {
                return true;
            }       
        }

        public IEnumerator GetEnumerator()//ekrana yazdirmak icin gerekli method, interface kullanarak
        {
            ArrayList array = new ArrayList();
            for (Node traveler = First; traveler != null; traveler = traveler.next)
            {
                array.Add("Menünüz: " + traveler.menu_adi);
                array.Add("Sandviciniz: " + traveler.sandvic);
                array.Add("Patates seciminiz: " + traveler.patates_secimi);
                array.Add("Fiyati: " + traveler.fiyat + "\n-----------------------");
            }
            return array.GetEnumerator();
        }
    }
}