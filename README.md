# ScreenWatcher v1.0

ScreenWatcher, arka planda (sistem tepsisinde) çalışan, hızlı ve özelleştirilebilir bir ekran yakalama aracıdır. Windows XP ve üzeri tüm sürümlerle uyumlu çalışması için **.NET Framework 4.0** altyapısı ile geliştirilmiştir.

**Geliştirici:** aoaydin

## Özellikler

- **Hızlı Ekran Yakalama:** `PrintScreen` tuşuna veya sizin belirlediğiniz özel bir kısayol tuşuna (`Ctrl`, `Alt`, `Shift` + herhangi bir tuş) basarak anında tam ekran görüntüsü alabilirsiniz.
- **Dinamik Dosya İsimlendirme:** Program ayarlarında belirlediğiniz alan başlıklarına göre (Örn: `Firma Adı`, `Döküm Kodu`), her ekran görüntüsü alındığında karşınıza bu alanları doldurabileceğiniz özel bir pencere gelir. Klavyeden hızlıca bu bilgileri doldurup `Enter` tuşuna basarak ekran görüntülerini bu alanlardan oluşan isimlerle kaydedebilirsiniz.
- **Esnek Kayıt Formatı:** Alınan ekran görüntülerini **PNG** formatında resim olarak veya **PDF** dokümanı olarak belirlediğiniz hedef klasöre otomatik kaydedebilirsiniz.
- **Arka Planda Çalışma:** Uygulama, ekranın sağ alt köşesindeki Sistem Tepsisinde (System Tray) sessizce çalışır ve bilgisayarınızın performansını etkilemez.

## Kurulum ve Kullanım

1. Uygulamayı başlattığınızda sistem tepsisinde bir ikon belirecektir.
2. Bu ikona sağ tıklayıp **"Ayarlar"** menüsünü açın.
3. Ayarlar sayfasında:
   - Kayıt edilecek hedef klasörü,
   - Kayıt formatını (PDF/PNG),
   - Sormasını istediğiniz "Dosya Adı Alanlarını" (Her satıra bir tane olacak şekilde, Örn: `Müşteri Adı`, `Referans Kodu`)
   - Ve ek kısayol tuşunuzu belirleyin.
4. "Kaydet ve Kapat" dedikten sonra programı arka plana gizleyebilirsiniz.
5. Ekran görüntüsü almak istediğinizde belirlediğiniz kısayol tuşuna basın, çıkan pencereye değerleri yazıp `Enter` tuşuyla seri bir şekilde dosyalarınızı kaydedin.

## Teknik Altyapı
- **Dil:** C# (WPF)
- **Framework:** .NET Framework 4.0
- **Kütüphaneler:** PdfSharp (PDF oluşturma), Hardcodet.NotifyIcon.Wpf (Sistem Tepsisi entegrasyonu), Newtonsoft.Json (Ayar yönetimi).

---

*Bu proje, iş akışlarını hızlandırmak ve manuel isimlendirme hatalarını önlemek amacıyla özel olarak tasarlanmıştır.*
