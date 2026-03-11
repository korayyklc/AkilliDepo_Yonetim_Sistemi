import React, { useEffect, useState } from 'react';
import { 
  Container, Box, Card, CardContent, Typography, Table, TableBody, 
  TableCell, TableContainer, TableHead, TableRow, Paper, Button, 
  TablePagination, TextField, Dialog, DialogTitle, DialogContent, DialogActions, Stack, Snackbar, Alert, Chip 
} from '@mui/material';
import api from './api';

interface Product { id: number; name: string; skuCode: string; stockQuantity: number; }

const COMPANY_ID = "DEPOTEST_001";

function App() {
  const [products, setProducts] = useState<Product[]>([]);
  const [totalCount, setTotalCount] = useState(0);
  const [page, setPage] = useState(0);
  const [rowsPerPage, setRowsPerPage] = useState(25);
  const [searchTerm, setSearchTerm] = useState('');
  
  // Bildirim State'i
  const [notify, setNotify] = useState({ open: false, msg: '', color: 'success' as 'success' | 'error' });
  const showMsg = (msg: string, color: 'success' | 'error' = 'success') => setNotify({ open: true, msg, color });

  const [open, setOpen] = useState(false);
  const [editId, setEditId] = useState<number | null>(null);
  const [formName, setFormName] = useState('');
  const [formSku, setFormSku] = useState('');

  const fetchProducts = async () => {
    try {
      const res = await api.get(`/products/by-company/${COMPANY_ID}`, {
        params: { page: page + 1, pageSize: rowsPerPage, searchTerm }
      });
      setProducts(res.data.data || res.data.Data || []);
      setTotalCount(res.data.totalCount || res.data.TotalCount || 0);
    } catch (e) { console.error(e); }
  };

  useEffect(() => { fetchProducts(); }, [page, rowsPerPage, searchTerm]);

  const handleSave = async () => {
    if (!formName || !formSku) return;
    const url = editId ? '/products/update' : '/products/create';
    const body = editId ? { Id: editId, CompanyId: COMPANY_ID, Name: formName, SkuCode: formSku } : { CompanyId: COMPANY_ID, Name: formName, SkuCode: formSku };
    
    await api.post(url, body);
    showMsg(editId ? "Ürün güncellendi" : "Yeni ürün eklendi");
    handleClose(); fetchProducts();
  };

  const handleDelete = async (id: number) => {
    if (window.confirm("Silinsin mi?")) {
      await api.post('/products/delete', { Id: id, CompanyId: COMPANY_ID });
      showMsg("Ürün silindi", "error");
      fetchProducts();
    }
  };

  const handleUpdateStock = async (id: number) => {
    const val = prompt("Yeni stok:");
    if (val) {
      await api.post('/products/update-stock', { Id: id, CompanyId: COMPANY_ID, NewQuantity: parseInt(val) });
      showMsg("Stok güncellendi");
      fetchProducts();
    }
  };

  const handleClose = () => { setOpen(false); setEditId(null); setFormName(''); setFormSku(''); };

  return (
    <Container maxWidth="lg" sx={{ mt: 4 }}>
      <Typography variant="h4" sx={{ mb: 3, fontWeight: 'bold', color: '#1976d2' }}>AKILLI DEPO SİSTEMİ</Typography>

      <Stack direction="row" spacing={2} sx={{ mb: 4 }}>
        <Card sx={{ bgcolor: '#1976d2', color: 'white', minWidth: 200 }}>
          <CardContent>
            <Typography variant="subtitle2">Kayıtlı Ürün Türü</Typography>
            <Typography variant="h3">{totalCount}</Typography>
          </CardContent>
        </Card>
      </Stack>

      <Paper sx={{ p: 2, mb: 3, display: 'flex', gap: 2 }}>
        <TextField label="Hızlı Ara (İsim/SKU)..." fullWidth size="small" onChange={(e) => setSearchTerm(e.target.value)} />
        <Button variant="contained" onClick={() => setOpen(true)}>YENİ ÜRÜN EKLE</Button>
      </Paper>

      <TableContainer component={Paper} sx={{boxShadow: 3}}>
        <Table>
          <TableHead sx={{ bgcolor: '#f5f5f5' }}>
            <TableRow>
              <TableCell><b>Ürün Adı</b></TableCell>
              <TableCell><b>SKU</b></TableCell>
              <TableCell align="right"><b>Stok Durumu</b></TableCell>
              <TableCell align="center"><b>Yönetim</b></TableCell>
            </TableRow>
          </TableHead>
          <TableBody>
            {products.map((p) => (
              <TableRow key={p.id} hover sx={{ bgcolor: p.stockQuantity < 5 ? '#fff5f5' : 'inherit' }}>
                <TableCell>{p.name}</TableCell>
                <TableCell>{p.skuCode}</TableCell>
                <TableCell align="right">
                  <Stack direction="row" spacing={1} justifyContent="flex-end" alignItems="center">
                    {p.stockQuantity < 5 && <Chip label="KRİTİK" size="small" color="error" variant="outlined" />}
                    <Typography sx={{ fontWeight: 'bold', color: p.stockQuantity > 0 ? 'green' : 'red' }}>
                        {p.stockQuantity}
                    </Typography>
                  </Stack>
                </TableCell>
                <TableCell align="center">
                  <Stack direction="row" spacing={1} justifyContent="center">
                    <Button variant="text" size="small" onClick={() => handleUpdateStock(p.id)}>STOK</Button>
                    <Button variant="text" size="small" onClick={() => { setEditId(p.id); setFormName(p.name); setFormSku(p.skuCode); setOpen(true); }}>DÜZENLE</Button>
                    <Button variant="text" size="small" color="error" onClick={() => handleDelete(p.id)}>SİL</Button>
                  </Stack>
                </TableCell>
              </TableRow>
            ))}
          </TableBody>
        </Table>
        <TablePagination component="div" count={totalCount} rowsPerPage={rowsPerPage} page={page} onPageChange={(_, n) => setPage(n)} onRowsPerPageChange={(e) => setRowsPerPage(parseInt(e.target.value, 10))} />
      </TableContainer>

      {/* BİLDİRİM KUTUSU */}
      <Snackbar open={notify.open} autoHideDuration={3000} onClose={() => setNotify({ ...notify, open: false })}>
        <Alert severity={notify.color} variant="filled">{notify.msg}</Alert>
      </Snackbar>

      <Dialog open={open} onClose={handleClose}>
        <DialogTitle>{editId ? 'Ürünü Güncelle' : 'Yeni Ürün Kaydı'}</DialogTitle>
        <DialogContent sx={{pt:2}}>
          <TextField fullWidth label="Ürün İsmi" sx={{mb:2, mt:1}} value={formName} onChange={(e) => setFormName(e.target.value)} />
          <TextField fullWidth label="SKU Kodu" value={formSku} onChange={(e) => setFormSku(e.target.value)} />
        </DialogContent>
        <DialogActions>
          <Button onClick={handleClose}>İptal</Button>
          <Button variant="contained" onClick={handleSave}>Kaydet</Button>
        </DialogActions>
      </Dialog>
    </Container>
  );
}

export default App;