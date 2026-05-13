Sebelum Injection (Data Kosong)

Saat tidak ada input atau filter tertentu, data tidak ditampilkan.

<img width="1919" height="1199" alt="image" src="https://github.com/user-attachments/assets/0723e4d5-b4a8-47a4-8f89-d408dc1ec0f2" />



Dengan memasukkan input tertentu seperti ' OR 1=1 --, seluruh data berhasil ditampilkan.

<img width="1919" height="1199" alt="image" src="https://github.com/user-attachments/assets/e11467f8-3326-43dc-b05f-302fd52d56e9" />

Query yang dihasilkan dari ' OR 1=1 --

SELECT *
FROM vw_DaftarBuku
WHERE judul LIKE '%' OR 1=1 -- %'
   OR pengarang LIKE '%...'
