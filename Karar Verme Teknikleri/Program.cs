using System;

class Program
{
    static void Main()
    {
        //Kullanıcıdan satır ve sütun sayısını alalım.
        Console.WriteLine("Kaç tane alternatifimiz(satır) olacak?");
        int rows = int.Parse(Console.ReadLine());

        Console.WriteLine("Kaç tane Talep eğilim seçeneğimiz(sütun) olacak?");
        int columns = int.Parse(Console.ReadLine());

        //Satır ve sutun isimlerini tutacak dizileri oluşturlaım.
        string[] rowHeaders = new string[rows];
        string[] columnHeaders = new string[columns];

        //satır isimlerini alalım
        for (int i = 0; i < rows; i++)
        {
            Console.WriteLine((i + 1) + ".alternatifimiz nedir?");
            rowHeaders[i] = Console.ReadLine();
        }

        //sutun isimlerini alalım.
        for (int i = 0; i < columns; i++)
        {
            Console.WriteLine((i + 1) + ".seçeneğimiz nedir?");
            columnHeaders[i] = Console.ReadLine();
        }

        //İki boyutlu bir dizi oluşturalım.
        double[,] data = new double[rows, columns];

        //Beklenti tablosunu dolduralım.
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                Console.WriteLine($"{rowHeaders[i]} ve {columnHeaders[j]} bloğunun değeri kaçtır? ");
                data[i, j] = double.Parse(Console.ReadLine());
            }
        }

        

        // Sütun başlıklarını hizalayarak yazdır
        Console.Write("".PadRight(18)); // İlk satır için satır başlıklarına yer bırak
        foreach (string colHeader in columnHeaders)
        {
            Console.Write(colHeader.PadRight(16)); // Sütun başlıklarını hizala
        }
        Console.WriteLine();

        // Satır başlıklarını ve verileri hizalayarak yazdır
        for (int i = 0; i < data.GetLength(0); i++) // Satır döngüsü
        {
            Console.Write(rowHeaders[i].PadRight(18)); // Satır başlığını hizala
            for (int j = 0; j < data.GetLength(1); j++) // Sütun döngüsü
            {
                Console.Write(data[i, j].ToString().PadRight(16)); // Değeri hizalayarak yazdır
            }
            Console.WriteLine();
        }

        Console.WriteLine("\n1. İyimserlik Ölçütü (Gelir Tablosu ise)");
        Console.WriteLine("2. İyimserlik Ölçütü (Maliyet Tablosu ise)");
        Console.WriteLine("3.Kötümserlik Ölçütü(Gelir Tablosu ise)");
        Console.WriteLine("4.Kötümserlik Ölçütü(Maliyet Tablosu ise)");
        Console.WriteLine("5.Eş Olasılık(Laplace) Ölçütü(Gelir Tablosu ise)");
        Console.WriteLine("6.Eş Olasılık(Laplace) Ölçütü(Maliyet Tablosu ise)");
        Console.WriteLine("7.Uzlaşma(Hurwics) Ölçütü(Gelir Tablosu ise)");
        Console.WriteLine("8.Uzlaşma(Hurwics) Ölçütü(Maliyet Tablosu ise");
        Console.WriteLine("9.Pişmanlık(Savage) Ölçütü(Gelir Tablosu ise");
        Console.WriteLine("10.Pişmanlık(Savage) Ölçütü(Maliyet Tablosu ise");

        Console.Write("Uygulanması gereken hesaplama yöntemini seçin: ");
        int islem = int.Parse(Console.ReadLine());

        if (islem == 1)
        {
            Console.WriteLine("\nİyimserlik Ölçütü (Gelir Tablosu):");
            double overallMax = double.MinValue; // Tüm satırlardaki maksimum değerlerin en büyüğü
            int sonAlternatif = -1; // Tercih edilen alternatifin indeksini tutacak değişken

            for (int i = 0; i < data.GetLength(0); i++)
            {
                double max = Maximum(data, i); // Her satır için iyimserlik hesapla
                Console.WriteLine($"{rowHeaders[i]}: En Yüksek Gelir = {max}");

                // Yeni bir maksimum bulunursa, bunu kaydet
                if (max > overallMax)
                {
                    overallMax = max;
                    sonAlternatif = i; // İlgili satırın indeksini sakla
                }
            }

            // Sonuç yazdırılırken tercih edilen satır belirtilir
            Console.WriteLine($"\nİyimserlik ölçütüne göre tercih edilmesi gereken alternatif: {rowHeaders[sonAlternatif]} ({overallMax})");
        }

        else if (islem == 2)
        {
            Console.WriteLine("\nİyimserlik Ölçütü (Maliyet Tablosu):");
            double overallMin = double.MaxValue; // Tüm satırlardaki minimum değerlerin en küçüğü
            int sonAlternatif = -1; // Tercih edilen alternatifin indeksini tutacak değişken

            for (int i = 0; i < data.GetLength(0); i++)
            {
                double min = Minimum(data, i); // Her satır için maliyet iyimserliği hesapla
                Console.WriteLine($"{rowHeaders[i]}: En Düşük Maliyet = {min}");

                // Yeni bir minimum bulunursa, bunu kaydet
                if (min < overallMin)
                {
                    overallMin = min;
                    sonAlternatif = i; // İlgili satırın indeksini sakla
                }
            }

            // Sonuç yazdırılırken tercih edilen satır belirtilir
            Console.WriteLine($"\nİyimserlik ölçütüne göre tercih edilmesi gereken alternatif: {rowHeaders[sonAlternatif]} ({overallMin})");
        }
        else if (islem == 3)
        {
            Console.WriteLine("\nKötümserlik Ölçütü (Gelir Tablosu):");
            double overallMaxOfMins = double.MinValue; // Tüm satırlardaki minimum değerlerin en büyüğü
            int sonAlternatif = -1; // Tercih edilen alternatifin indeksini tutacak değişken

            for (int i = 0; i < data.GetLength(0); i++)
            {
                double min = Minimum(data, i); // Her satırdaki en düşük değeri bul
                Console.WriteLine($"{rowHeaders[i]}: En Düşük Gelir = {min}");

                // Bulunan minimum değeri en büyük minimum değerle karşılaştır
                if (min > overallMaxOfMins)
                {
                    overallMaxOfMins = min;  // En büyük minimum değeri kaydet
                    sonAlternatif = i;   // İlgili satırın indeksini sakla
                }
            }

            // Sonuç yazdırılırken tercih edilen satır belirtilir
            Console.WriteLine($"\nKötümserlik ölçütüne göre tercih edilmesi gereken alternatif: {rowHeaders[sonAlternatif]} ({overallMaxOfMins})");
        }
        else if (islem == 4)
{
    Console.WriteLine("\nKötümserlik Ölçütü (Maliyet Tablosu):");
    double overallMinOfMaxs = double.MaxValue; // Tüm satırlardaki maksimum değerlerin en küçüğü
    int sonAlternatif = -1; // Tercih edilen alternatifin indeksini tutacak değişken

    for (int i = 0; i < data.GetLength(0); i++)
    {
        double max = Maximum(data, i); // Her satırdaki en büyük değeri bul
        Console.WriteLine($"{rowHeaders[i]}: En Yüksek Maliyet = {max}");

        // Bulunan maksimum değeri en küçük maksimum değerle karşılaştır
        if (max < overallMinOfMaxs)
        {
            overallMinOfMaxs = max;  // En küçük maksimum değeri kaydet
                    sonAlternatif = i;   // İlgili satırın indeksini sakla
        }
    }

    // Sonuç yazdırılırken tercih edilen satır belirtilir
    Console.WriteLine($"\nKötümserlik ölçütüne göre tercih edilmesi gereken alternatif: {rowHeaders[sonAlternatif]} ({overallMinOfMaxs})");
}
        else if(islem == 5)
        {
            Console.WriteLine("\nEş Olasılık Ölçütü (Gelir Tablosu):");

            double overallMaxAvg = double.MinValue; // Tüm satırlardaki ortalamaların en büyüğü
            int sonAlternatif = -1; // Tercih edilen alternatifin indeksini tutacak değişken

            for (int i = 0; i < data.GetLength(0); i++)
            {
                double avg = Ortalama(data, i); // Her satır için ortalama hesapla
                Console.WriteLine($"{rowHeaders[i]}: Ortalama Gelir = {avg}");

                // Yeni bir maksimum ortalama bulunursa, bunu kaydet
                if (avg > overallMaxAvg)
                {
                    overallMaxAvg = avg;
                    sonAlternatif = i; // İlgili satırın indeksini sakla
                }
            }

            // Sonuç yazdırılırken tercih edilen satır belirtilir
            Console.WriteLine($"\nEş Olasılık ölçütüne göre tercih edilmesi gereken alternatif: {rowHeaders[sonAlternatif]} ({overallMaxAvg})");
        }
        else if ( islem == 6)
        {
            Console.WriteLine("\nEş Olasılık Ölçütü (Maliyet Tablosu):");

            double overallMinAvg = double.MaxValue; // Tüm satırlardaki ortalamaların en büyüğü
            int sonAlternatif = -1; // Tercih edilen alternatifin indeksini tutacak değişken

            for (int i = 0; i < data.GetLength(0); i++)
            {
                double avg = Ortalama(data, i); // Her satır için ortalama hesapla
                Console.WriteLine($"{rowHeaders[i]}: Ortalama Maliyet = {avg}");

                // Yeni bir maksimum ortalama bulunursa, bunu kaydet
                if (avg < overallMinAvg)
                {
                    overallMinAvg = avg;
                    sonAlternatif = i; // İlgili satırın indeksini sakla
                }
            }

            // Sonuç yazdırılırken tercih edilen satır belirtilir
            Console.WriteLine($"\nEş Olasılık ölçütüne göre tercih edilmesi gereken alternatif: {rowHeaders[sonAlternatif]} ({overallMinAvg})");
        }
        else if (islem == 7)
        {          
            Console.Write("\nHurwicz katsayısını giriniz (0 ile 1 arasında): ");
            double alpha = double.Parse(Console.ReadLine());

            if (alpha < 0 || alpha > 1)
            {
                Console.WriteLine("Hurwicz katsayısı 0 ile 1 arasında olmalıdır!");
                return;
            }

            Console.WriteLine("\nHurwicz Ölçütü Hesaplama (Gelir Tablosu):");

            double overallMaxHurwicz = double.MinValue; // Hurwicz değeri en büyük olan satırı bulmak için
            int sonAlternatif = -1; // Tercih edilen alternatifin indeksini tutacak değişken

            for (int i = 0; i < data.GetLength(0); i++)
            {
                double max = Maximum(data, i); // Satırdaki en büyük değer
                double min = Minimum(data, i); // Satırdaki en küçük değer
                double hurwiczValue = alpha * max + (1 - alpha) * min; // Hurwicz formülü

                Console.WriteLine($"{rowHeaders[i]}: Hurwicz Değeri = {hurwiczValue}");

                // En yüksek Hurwicz değerini bul
                if (hurwiczValue > overallMaxHurwicz)
                {
                    overallMaxHurwicz = hurwiczValue;
                    sonAlternatif = i; // İlgili satırın indeksini sakla
                }
            }

            // Sonuç yazdırılırken tercih edilen satır belirtilir
            Console.WriteLine($"\nHurwicz ölçütüne göre tercih edilmesi gereken alternatif: {rowHeaders[sonAlternatif]} ({overallMaxHurwicz})");
        }
        else if (islem == 8)
        {
            Console.Write("\nHurwicz katsayısını giriniz (0 ile 1 arasında): ");
            double alpha = double.Parse(Console.ReadLine());

            if (alpha < 0 || alpha > 1)
            {
                Console.WriteLine("Hurwicz katsayısı 0 ile 1 arasında olmalıdır!");
                return;
            }

            Console.WriteLine("\nHurwicz Ölçütü Hesaplama (Maliyet Tablosu):");

            double overallMinHurwicz = double.MaxValue; // Hurwicz değeri en küçük olan satırı bulmak için
            int sonAlternatif = -1; // Tercih edilen alternatifin indeksini tutacak değişken

            for (int i = 0; i < data.GetLength(0); i++)
            {
                double max = Maximum(data, i); // Satırdaki en büyük değer
                double min = Minimum(data, i); // Satırdaki en küçük değer
                double hurwiczValue = alpha * min + (1 - alpha) * max; // Hurwicz formülü (maliyet için)

                Console.WriteLine($"{rowHeaders[i]}: Hurwicz Değeri = {hurwiczValue}");

                // En düşük Hurwicz değerini bul
                if (hurwiczValue < overallMinHurwicz)
                {
                    overallMinHurwicz = hurwiczValue;
                    sonAlternatif = i; // İlgili satırın indeksini sakla
                }
            }

            // Sonuç yazdırılırken tercih edilen satır belirtilir
            Console.WriteLine($"\nHurwicz ölçütüne göre tercih edilmesi gereken alternatif: {rowHeaders[sonAlternatif]} ({overallMinHurwicz})");
        }
        
        else if (islem == 9)
        {
            Console.WriteLine("\nSavage (Pişmanlık) Ölçütü (Gelir Tablosu):");

            // Pişmanlık tablosunu hesaplama
            double[,] regretTable = new double[data.GetLength(0), data.GetLength(1)];
            for (int j = 0; j < data.GetLength(1); j++)
            {
                double maxInColumn = double.MinValue;

                // Her sütun için maksimum değeri bul
                for (int i = 0; i < data.GetLength(0); i++)
                {
                    if (data[i, j] > maxInColumn)
                        maxInColumn = data[i, j];
                }

                // Pişmanlık değerlerini hesapla
                for (int i = 0; i < data.GetLength(0); i++)
                {
                    regretTable[i, j] = maxInColumn - data[i, j];
                }
            }

            double overallMinOfMaxRegret = double.MaxValue; // Pişmanlık değerlerinin en küçüğünü bulmak için
            int sonAlternatif = -1;

            // Her satırdaki maksimum pişmanlığı bul ve tercihi yap
            for (int i = 0; i < regretTable.GetLength(0); i++)
            {
                double maxRegret = Maximum(regretTable, i);
                Console.WriteLine($"{rowHeaders[i]}: En Büyük Pişmanlık = {maxRegret}");

                if (maxRegret < overallMinOfMaxRegret)
                {
                    overallMinOfMaxRegret = maxRegret;
                    sonAlternatif = i;
                }
            }

            Console.WriteLine($"\nSavage ölçütüne göre tercih edilmesi gereken alternatif: {rowHeaders[sonAlternatif]} ({overallMinOfMaxRegret})");
        }
        else if (islem == 10)
        {
            Console.WriteLine("\nSavage (Pişmanlık) Ölçütü (Maliyet Tablosu):");

            // Pişmanlık tablosunu hesaplama
            double[,] regretTable = new double[data.GetLength(0), data.GetLength(1)];
            for (int j = 0; j < data.GetLength(1); j++)
            {
                double minInColumn = double.MaxValue;

                // Her sütun için minimum değeri bul
                for (int i = 0; i < data.GetLength(0); i++)
                {
                    if (data[i, j] < minInColumn)
                        minInColumn = data[i, j];
                }

                // Pişmanlık değerlerini hesapla
                for (int i = 0; i < data.GetLength(0); i++)
                {
                    regretTable[i, j] = data[i, j] - minInColumn;
                }
            }

            double overallMinOfMaxRegret = double.MaxValue; // Pişmanlık değerlerinin en küçüğünü bulmak için
            int sonAlternatif = -1;

            // Her satırdaki maksimum pişmanlığı bul ve tercihi yap
            for (int i = 0; i < regretTable.GetLength(0); i++)
            {
                double maxRegret = Maximum(regretTable, i);
                Console.WriteLine($"{rowHeaders[i]}: En Büyük Pişmanlık = {maxRegret}");

                if (maxRegret < overallMinOfMaxRegret)
                {
                    overallMinOfMaxRegret = maxRegret;
                    sonAlternatif = i;
                }
            }

            Console.WriteLine($"\nSavage ölçütüne göre tercih edilmesi gereken alternatif: {rowHeaders[sonAlternatif]} ({overallMinOfMaxRegret})");
        }
        




    }

    // Belirli bir satırdaki en yüksek değeri bulan fonksiyon
    public static double Maximum(double[,] data, int rowIndex)
    {
        double max = data[rowIndex, 0];
        for (int j = 1; j < data.GetLength(1); j++)
        {
            if (data[rowIndex, j] > max)
                max = data[rowIndex, j];
        }
        return max;
    }

    // Belirli bir satırdaki en düşük değeri bulan fonksiyon
    public static double Minimum(double[,] data, int rowIndex)
    {
        double min = data[rowIndex, 0];
        for (int j = 1; j < data.GetLength(1); j++)
        {
            if (data[rowIndex, j] < min)
                min = data[rowIndex, j];
        }
        return min;
    }
    // Satırdaki ortalamayı hesaplayan fonksiyon
    public static double Ortalama(double[,] data, int rowIndex)
    {
        double toplam = 0;
        for (int j = 0; j < data.GetLength(1); j++)
        {
            toplam += data[rowIndex, j]; // Satırdaki her öğeyi toplar
        }
        return toplam / data.GetLength(1); // Toplamı, öğe sayısına bölerek ortalamayı döndürür
    }
}
