int Konum;
int Bekleme;
int gelen;
int sonveri = 0;
void setup() { 
   
  pinMode(8, OUTPUT);
  pinMode(9, OUTPUT);
  pinMode(10, OUTPUT);
  pinMode(11, OUTPUT);

  digitalWrite(8, LOW);
  digitalWrite(9, LOW);
  digitalWrite(10, LOW);
  digitalWrite(11, LOW);
  Konum = 8; // Oanda hangi bobin aktif
  Bekleme = 5; // Motorun Hızı
    Serial.begin(9600);
}

void loop() {
  if(Serial.available()) // Serial haberleşmenin aktif olup olmadığının kontrolü
    {
        gelen = Serial.read(); // Serialden gelen veri "gelen" değişkenine yazılır
    
        if((gelen < 256) && (gelen != sonveri) && (gelen > 150))
          {
          solaDon (10); 
          sonveri = gelen;
          }
           if((gelen < 90) && (gelen != 0) && (gelen != sonveri)) 
        {
          sagaDon(10);
          sonveri = gelen;
        }
                  
    else //if ((gelen > 78) && (gelen < 178))
{
  digitalWrite(8, LOW);
  digitalWrite(9, LOW);
  digitalWrite(10, LOW);
  digitalWrite(11, LOW);
     delay(Bekleme);

}
   
}
}
void solaDon (int Adim) {
  for (int i = 0; i < Adim; i++) {
    digitalWrite (Konum,HIGH);
    delay(Bekleme);
    digitalWrite (Konum,LOW);
    KonumArttir();
    }
  }
  void sagaDon (int Adim) {
  for (int i = 0; i < Adim; i++) {
    digitalWrite (Konum,HIGH);
    delay(Bekleme);
    digitalWrite (Konum,LOW);
    KonumAzalt();
    }
  }
  void KonumArttir() {
    Konum++;
    if (Konum == 12) Konum = 8;
    }
     void KonumAzalt() {
    Konum--;
    if (Konum == 7) Konum = 11;
    }
