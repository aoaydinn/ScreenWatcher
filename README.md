# ScreenWatcher

ScreenWatcher, sistem tepsisinde arka planda çalışan, **Print Screen** ve özelleştirilebilir **global kısayol** ile tam ekran görüntüsü alıp **PNG** veya **PDF** olarak kaydeden bir Windows aracıdır. **.NET Framework 4.0** hedeflenir; dağıtım için uygun geriye dönük uyumluluk sağlanır.

**Sürüm:** 1.0.0 · **Geliştirici:** aoaydin

## Özellikler

- **İki kısayol:** Varsayılan olarak **Print Screen** ve ayarlardan tanımladığınız **özel kısayol** (ör. Ctrl+Shift+F12) birlikte kayıtlıdır; ikisi de aynı yakalama akışını tetikler.
- **Özel kısayol:** Ctrl, Alt, Shift ve Win için **çoklu seçim** (onay kutuları) ve daraltılmış **tuş listesi** (harf, rakam, F1–F24, Space, Enter, Tab, Escape).
- **Kayıt klasörü ve format:** Hedef klasör, **PNG** veya **PDF**; dosya adları zaman damgası ile benzersizdir.
- **İsteğe bağlı ad alanları:** Ayarlarda “dosya adında sorulacak alanlar” satırları tanımlıysanız, her yakalamada bu alanları girebileceğiniz bir pencere açılır; **Enter** ile hızlı onay mümkündür. Alan tanımlı değilse görüntü doğrudan `EkranGoruntusu_…` önekiyle kaydedilir.
- **Sistem tepsisi:** Sağ tık menüsü: **Ayarlar**, **Hakkında** (sürüm bilgisi), **Çıkış**.
- **Ayarların kalıcılığı:** `%AppData%\ScreenWatcher\settings.json` dosyasına yazılır. Kayıt veya dosya yazımı başarısız olursa uygulama kullanıcıya uyarı gösterir; ayarlar diske yazılamadıysa kısayol güncellemesi yapılmaz.

## Kurulum ve kullanım

1. `ScreenWatcher.exe` dosyasını çalıştırın; sistem tepsisinde ikon belirir.
2. İkona **sağ tıklayıp** **Ayarlar**’ı açın.
3. **Kaydedilecek klasör**, **kayıt formatı** (PNG/PDF), isteğe bağlı **dosya adı alanları** (her satıra bir etiket) ve **ek kısayol** (modifier + tuş) değerlerini girin.
4. **Kaydet ve Kapat** ile ayarları diske yazın ve pencereyi kapatın; kısayollar bu andan itibaren güncellenir. İsterseniz **Arka Plana Gizle** ile pencereyi kapatmadan simgeye indirebilirsiniz.
5. **Print Screen** veya tanımlı özel kısayola basarak ekran görüntüsü alın; alanlar tanımlıysa açılan pencerede değerleri girip onaylayın.

## Gereksinimler

- Windows (WPF ve global kısayol API’leri için uygun bir Windows sürümü).
- Çalışma zamanı: **.NET Framework 4.0** veya üzeri (hedef çerçeve 4.0).

## Geliştirici: derleme

```text
dotnet build ScreenWatcher.csproj -c Release
```

Çıktı: `bin\Release\ScreenWatcher.exe` (hedef çerçeveye göre alt klasör yapısı değişebilir).

## Teknik özet

| Alan | Açıklama |
|------|----------|
| Dil / UI | C#, WPF |
| Hedef çerçeve | .NET Framework 4.0 |
| PDF | PdfSharp |
| Tepsi ikonu | Hardcodet.NotifyIcon.Wpf |
| Ayarlar | Newtonsoft.Json |
| Gömülü bağımlılıklar | Costura.Fody / Fody (isteğe bağlı paket yapılandırması) |
| Global kısayol | `RegisterHotKey` / `UnregisterHotKey`, pencere mesaj kancası |

---

*İş akışını hızlandırmak ve dosya adlandırma tutarlılığını artırmak için tasarlanmıştır.*
