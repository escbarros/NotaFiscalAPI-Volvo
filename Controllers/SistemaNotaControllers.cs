using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using WebApiNotaFiscal.Models;
using WebApiNotaFiscal.Controllers;


namespace WebApiNotaFiscal.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class SistemaNotaControllers : Controller
    {
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("/error")]
        public IActionResult HandleError(){
            StreamWriter sw = new StreamWriter(@"Controllers\erros.txt", true);
            try{
                throw new ConcessionariaException("Erro ao criar objeto");
            }catch(Exception e){
                // System.Console.WriteLine(e.Message);
                sw.WriteLine($"Erro ao criar objeto");
                sw.WriteLine("Exeção: "+e.Message);
                sw.WriteLine("Série de chamada: "+e.StackTrace);
                sw.WriteLine("Método: "+e.TargetSite + "\n");
                sw.Close();
                return BadRequest();
            }
        }

        SistemaConcessionariaTratamento tratamento = new SistemaConcessionariaTratamento();
        [HttpPost("Post/Concessionaria")]
        
        public IActionResult PostConcessionaria([FromBody] Concessionaria concessionaria){
            StreamWriter sw = new StreamWriter(@"Controllers\erros.txt", true);
            try{
                if(ModelState.IsValid){
                    using(var context = new SistemaNotaFiscal()){
                        tratamento.ConcessionariaTratamento(concessionaria);
                        context.Concessionaria.Add(concessionaria);
                        context.SaveChanges();
                        return Ok(concessionaria);}
                    }
                else{
                    throw new ConcessionariaException("Erro ao criar concessionaria");
                }
            }catch(ConcessionariaException e){
                sw.WriteLine($"Erro ao criar concessionaria ({e.motivo})\n{e.TargetSite} {e.StackTrace}\n");
                sw.Close();
                return NotFound(e.motivo);
            }finally{
                sw.Close();
            }
        }

        [HttpPost("Post/Concessionaria/Endereco")] 
        public IActionResult PostConcessionariaEndereco([FromBody] EnderecoConcessionaria enderecoConcessionaria){
            StreamWriter sw = new StreamWriter(@"Controllers\erros.txt", true);
            try{
                if(ModelState.IsValid){
                    using(var context = new SistemaNotaFiscal()){
                        tratamento.EnderecoConcessionariaTratamento(enderecoConcessionaria);
                        context.EnderecoConcessionarias.Add(enderecoConcessionaria);
                        context.SaveChanges();
                        return Ok(enderecoConcessionaria);
                    }
                }
                else throw new ConcessionariaException("Erro ao criar endereco da concessionaria");
            }catch(ConcessionariaException e){
                sw.WriteLine($"Erro ao cadastrar endereco da concessionaria ({e.motivo})\n{e.TargetSite} {e.StackTrace}\n");
                sw.Close();
                return NotFound(e.motivo);
            }finally{
                sw.Close();
            }
            
        }

        [HttpPost("Post/Cliente/Endereco")]
        public IActionResult PostClienteEndereco([FromBody] Endereco endereco){
            StreamWriter sw = new StreamWriter(@"Controllers\erros.txt", true);
            try{
                if(ModelState.IsValid){
                    using(var context = new SistemaNotaFiscal()){
                        tratamento.EnderecoClienteTratamento(endereco);
                        context.Enderecos.Add(endereco);
                        context.SaveChanges();
                        return Ok(endereco);
                    }
                }
                else throw new ConcessionariaException("Erro ao criar Endereco de cliente");
            }catch(ConcessionariaException e){
                sw.WriteLine($"Erro ao cadastrar endereco do cliente ({e.motivo})\n{e.TargetSite} {e.StackTrace}\n");
                sw.Close();
                return NotFound(e.motivo);
            }finally{
                sw.Close();
            }
        }

        [HttpPost("Post/Cliente")]
        
        public IActionResult PostCliente([FromBody] Cliente cliente){
            StreamWriter sw = new StreamWriter(@"Controllers\erros.txt", true);
            try{
                if(ModelState.IsValid){
                    using(var context = new SistemaNotaFiscal()){
                        tratamento.ClienteTratamento(cliente);
                        context.Clientes.Add(cliente);
                        context.SaveChanges();
                        return Ok(cliente);
                    }
                }
                else throw new ConcessionariaException("Erro ao criar Cliente");
            }catch(ConcessionariaException e){
                sw.WriteLine($"Erro ao cadastrar Funcionario ({e.motivo})\n{e.TargetSite} {e.StackTrace}\n");
                sw.Close();
                return NotFound(e.motivo);
            }finally{
                sw.Close();
            }
        }

        [HttpPost("Post/Funcionario")]
        public IActionResult PostFuncionario(Funcionario funcionario){
            StreamWriter sw = new StreamWriter(@"Controllers\erros.txt", true);
            try{
                if(ModelState.IsValid){
                    using(var context = new SistemaNotaFiscal()){
                        context.Funcionarios.Add(funcionario);
                        context.SaveChanges();
                        return Ok(funcionario);
                    }
                }
                else throw new ConcessionariaException("Nao foi possivel criar o funcionario");
            }catch(ConcessionariaException e){
                sw.WriteLine($"Erro ao cadastrar Funcionario ({e.motivo})\n{e.TargetSite} {e.StackTrace}\n");
                sw.Close();
                return NotFound(e.motivo);
            }finally{
                sw.Close();
            }
        }

        [HttpPost("Post/Venda")]
        public IActionResult PostVenda([FromBody] Venda venda){
            StreamWriter sw = new StreamWriter(@"Controllers\erros.txt", true);
            try{
                if(ModelState.IsValid){
                    using(var context = new SistemaNotaFiscal()){
                        venda.ValorTotal = 0;
                        context.Venda.Add(venda);
                        context.SaveChanges();
                        return Ok(venda);
                    }
                }
                else throw new ConcessionariaException("Erro ao criar Venda");
            }catch(ConcessionariaException e){
                sw.WriteLine($"Erro ao cadastrar venda ({e.motivo})\n{e.TargetSite} {e.StackTrace}\n");
                sw.Close();
                return NotFound(e.motivo);
            }finally{
                sw.Close();
            }
        }

        [HttpPost("Post/Compra")]        
        public IActionResult PostCompra([FromBody] Compra compra){
            StreamWriter sw = new StreamWriter(@"Controllers\erros.txt", true);
            try{
                if(ModelState.IsValid){
                    using(var context = new SistemaNotaFiscal()){

                        var funcionario = context.Funcionarios.Find(compra.IdFuncionario);
                        var venda = context.Venda.Find(compra.IdVenda);
                        var produto = context.Produtos.Find(compra.IdProduto);
                        venda.ValorTotal += Convert.ToInt32(produto.Valor);
                        funcionario.Salario += Convert.ToInt32(venda.ValorTotal * 0.01);
                        context.Compras.Add(compra);
                        context.SaveChanges();
                        return Ok(compra);
                    }
                }
                else throw new ConcessionariaException("Erro ao criar Compra");
            }catch(ConcessionariaException e){
                sw.WriteLine($"Erro ao cadastrar compra ({e.motivo})\n{e.TargetSite} {e.StackTrace}\n");
                sw.Close();
                return NotFound(e.motivo);
            }finally{
                sw.Close();
            }
        }


        [HttpPost("Post/Venda/NotaFiscal")]
        
        public IActionResult PostNotaFiscal([FromBody] NotaFiscal notaFiscal){
            StreamWriter sw = new StreamWriter(@"Controllers\erros.txt", true);
            try{
                if(ModelState.IsValid){
                    using(var context = new SistemaNotaFiscal()){
                        if(notaFiscal.TributaçãoImposto > 100) throw new ConcessionariaException("Erro ao criar objeto");
                        context.NotaFiscals.Add(notaFiscal);
                        context.SaveChanges();
                        return Ok(notaFiscal);
                    }
                }
                else throw new ConcessionariaException("Erro ao criar objeto");
            }catch(ConcessionariaException e){
                sw.WriteLine($"Erro ao cadastrar nota fiscal ({e.motivo})\n{e.TargetSite} {e.StackTrace}\n");
                sw.Close();
                return NotFound(e.motivo);
            }finally{
                sw.Close();
            }
        }

        [HttpPost("Post/Produto")]
        
        public IActionResult PostProduto([FromBody] Produto produto){
            StreamWriter sw = new StreamWriter(@"Controllers\erros.txt", true);
            try{
                if(ModelState.IsValid){
                    using(var context = new SistemaNotaFiscal()){
                        tratamento.ProdutoTratamento(produto);
                        context.Produtos.Add(produto);
                        context.SaveChanges();
                        return Ok(produto);
                    }
                }
                else throw new ConcessionariaException("Erro ao criar Produto");
            }catch(ConcessionariaException e){
                sw.WriteLine($"Erro ao casastrar produto ({e.motivo})\n{e.TargetSite} {e.StackTrace}\n");
                sw.Close();
                return NotFound(e.motivo);
            }finally{
                sw.Close();
            }
        }

        [HttpGet("Get/NotaFiscal/Venda{id}")]
        
        public IActionResult getNF(int id){
            StreamWriter sw = new StreamWriter(@"Controllers\erros.txt", true);
            try{
                //Verifica se o id de venda existe
                using(var context = new SistemaNotaFiscal()){
                    var venda = context.Venda
                                    .FirstOrDefault(s => s.IdVenda == id);
                    if(venda == null){throw new ConcessionariaException("Nenhuma venda encontrada");}

                    var compra = context.Compras
                                    .FirstOrDefault(s => s.IdVenda == venda.IdVenda); 

                    if(compra == null){throw new ConcessionariaException("Nenhuma compra encontrada");}

                    var cliente = context.Clientes
                                    .FirstOrDefault(s => s.IdCliente == compra.IdCliente); 

                    if(cliente == null){throw new ConcessionariaException("Cliente nao encontrado");}

                    var endereco = context.Enderecos
                                    .FirstOrDefault(s => s.IdEndereco == cliente.IdEndereco); 

                    if(endereco == null){throw new ConcessionariaException("Endereco do cliente nao encontrado");}

                    var notaFiscal = context.NotaFiscals
                                    .FirstOrDefault(s => s.IdVenda == venda.IdVenda);
                    if(notaFiscal == null){throw new ConcessionariaException("Nota fiscal nao foi gerada");}

                    var concessionaria = context.Concessionaria
                                    .FirstOrDefault(s => s.IdConcessionaria == notaFiscal.IdConcessionaria);
                    if(concessionaria == null){throw new ConcessionariaException("Concessionaria nao encontrada");}

                    var enderecoConcessionaria = context.EnderecoConcessionarias
                                    .FirstOrDefault(s => s.IdConcessionaria == concessionaria.IdConcessionaria);
                    if(enderecoConcessionaria == null){throw new ConcessionariaException("Endereco da concessionaria nao encontrada");}
                    var produto = context.Produtos
                                .FirstOrDefault(s => s.idProduto == compra.IdProduto);
                    if(produto == null){throw new ConcessionariaException("Produto nao encontrado");}
                    
                    //Precisa montar o nf
                    Bitmap imageFile = new Bitmap(@"NotaFiscal\nota_fiscal_mod_11024_1.jpg");
                    var g =  Graphics.FromImage(imageFile);
                    //Concessionaria - Nomde/Razão Social
                    g.DrawString(concessionaria.RazaoSocial, new Font("Calibri", 10F, FontStyle.Regular), Brushes.Black, new PointF(240, 118));
                    //Concessionaria - Endereco
                    g.DrawString(enderecoConcessionaria.rua +", "+enderecoConcessionaria.NumeroRua, new Font("Calibri", 10F, FontStyle.Regular), Brushes.Black, new PointF(236, 155));
                    //Concessionaria - Bairro
                    g.DrawString(enderecoConcessionaria.bairro, new Font("Calibri", 10F, FontStyle.Regular), Brushes.Black, new PointF(474, 154));
                    //Concessionaria - Municipio
                    g.DrawString(enderecoConcessionaria.municipio, new Font("Calibri", 10F, FontStyle.Regular), Brushes.Black, new PointF(288, 174));
                    //Concessionaria - UF
                    g.DrawString(enderecoConcessionaria.uf, new Font("Calibri", 10F, FontStyle.Regular), Brushes.Black, new PointF(484, 177));
                    //Concessionaria - Telefone
                    g.DrawString(concessionaria.Telefone, new Font("Calibri", 10F, FontStyle.Regular), Brushes.Black, new PointF(222,216));
                    //Concessionaria - cep
                    g.DrawString(enderecoConcessionaria.Cep, new Font("Calibri", 8F, FontStyle.Regular), Brushes.Black, new PointF(441,213));
                    //Concessionaria - cnpj
                    g.DrawString(concessionaria.Cnpj, new Font("Calibri", 8F, FontStyle.Regular), Brushes.Black, new PointF(488,230));
                    //Cliente - Nomde/Razão Social
                    g.DrawString(cliente.Nome, new Font("Calibri", 8F, FontStyle.Regular), Brushes.Black, new PointF(67, 301));
                    //Cliente - Endereco
                    g.DrawString(endereco.rua +", "+endereco.NumeroRua, new Font("Calibri", 8F, FontStyle.Regular), Brushes.Black, new PointF(64, 354));
                    //Cliente - Bairro
                    g.DrawString(endereco.bairro, new Font("Calibri", 8F, FontStyle.Regular), Brushes.Black, new PointF(422, 325));
                    //Cliente - Municipio
                    g.DrawString(endereco.municipio, new Font("Calibri", 8F, FontStyle.Regular), Brushes.Black, new PointF(63, 354));
                    //Cliente - UF
                    g.DrawString(endereco.uf, new Font("Calibri", 8F, FontStyle.Regular), Brushes.Black, new PointF(464, 351));
                    //Cliente - Telefone
                    g.DrawString(cliente.TelefoneCliente, new Font("Calibri", 8F, FontStyle.Regular), Brushes.Black, new PointF(348,353));
                    //Cliente - cep
                    g.DrawString(endereco.Cep, new Font("Calibri", 8F, FontStyle.Regular), Brushes.Black, new PointF(560,325));
                    //cliente - cnpj
                    g.DrawString(cliente.CPF, new Font("Calibri", 8F, FontStyle.Regular), Brushes.Black, new PointF(521,296));
                    imageFile.Save(@"NotaFiscal\notaFiscal"+id+".jpg");
                    g.DrawString("0000"+notaFiscal.IdNotaFiscal, new Font("Calibri", 10F, FontStyle.Regular), Brushes.Black, new PointF(614,114));
                    g.DrawString("0000"+notaFiscal.IdNotaFiscal, new Font("Calibri", 6F, FontStyle.Regular), Brushes.Black, new PointF(678,931));

                    //cODIGO pRODUTO
                    g.DrawString("000"+compra.IdProduto, new Font("Calibri", 6F, FontStyle.Regular), Brushes.Black, new PointF(306,442));


                    //Descrição do produto
                    g.DrawString(produto.Fabricante+" "+produto.Modelo+" "+produto.cor, new Font("Calibri", 6F, FontStyle.Regular), Brushes.Black, new PointF(117,463));

                    //Unidade
                    g.DrawString("1", new Font("Calibri", 6F, FontStyle.Regular), Brushes.Black, new PointF(341,463));

                    //Quantidade
                    g.DrawString("1", new Font("Calibri", 6F, FontStyle.Regular), Brushes.Black, new PointF(383,463));

                    //Valor Unitario
                    g.DrawString("0"+produto.Valor, new Font("Calibri", 6F, FontStyle.Regular), Brushes.Black, new PointF(453,462));

                    //Valor Total
                    g.DrawString("0"+venda.ValorTotal, new Font("Calibri", 6F, FontStyle.Regular), Brushes.Black, new PointF(520,460));

                    //fatura 
                    g.DrawString(venda.tipoPagamento + " " + venda.Comentario, new Font("Calibri", 6F, FontStyle.Regular), Brushes.Black, new PointF(33,392));
                    return Ok();
                }
            }catch(ConcessionariaException e){
                sw.WriteLine($"Erro ao gerear nota fiscal ({e.motivo})\n{e.TargetSite} {e.StackTrace}\n");
                sw.Close();
                return NotFound(e.motivo);
            }finally{
                sw.Close();
            }
        }

        [HttpGet("Get/Funcionarios")]
        public IActionResult GetFuncionario(){
            using(var context = new SistemaNotaFiscal())
            {
                return Ok(context.Funcionarios.ToList());
            }
        }

        [HttpGet("Get/Produto")]
        public IActionResult GetProduto(){
            using(var context = new SistemaNotaFiscal()){
                return Ok(context.Produtos.ToList());
            }
        }

        [HttpGet("Get/Cliente")]
        
        public IActionResult GetCliente(){
            using(var context = new SistemaNotaFiscal()){
                    
                return Ok(context.Clientes.ToList());
            }
        }

        [HttpGet("Get/Compra")]
        
        public IActionResult GetCompra(){
            using(var context = new SistemaNotaFiscal()){
                    
                return Ok(context.Compras.ToList());
            }
        }

        [HttpGet("Get/Venda")]
        
        public IActionResult GetVenda(){
            using(var context = new SistemaNotaFiscal()){
                    
                return Ok(context.Venda.ToList());
            }
        }

        [HttpGet("Get/Concessionaria")]
        
        public IActionResult GetConcessionaria(){
            using(var context = new SistemaNotaFiscal()){
                    
                return Ok(context.Concessionaria.ToList());
            }
        }

        [HttpGet("Get/NotaFiscal")]
        public IActionResult GetNotaFiscal(){
            using(var context = new SistemaNotaFiscal()){
                return Ok(context.NotaFiscals.ToList());
            }
        }

        [HttpPut("Put/Cliente/{id}")]
        public IActionResult PutCliente(int id, [FromBody] Cliente novoCliente){
            StreamWriter sw = new StreamWriter(@"Controllers\erros.txt", true);
            try{
                if(ModelState.IsValid){
                    using(var context = new SistemaNotaFiscal()){
                        var cliente = context.Clientes.Find(id);
                        if(cliente ==null) throw new ConcessionariaException("Não existe nenhum cliente com esse id");
                        tratamento.ClienteTratamento(novoCliente);
                        context.Entry(cliente).CurrentValues.SetValues(novoCliente);
                        context.SaveChanges();
                        return Ok();
                    }
                }else throw new ConcessionariaException("Erro ao criar objeto");
            }catch(ConcessionariaException e){
                sw.WriteLine($"Erro ao atualizar cliente ({e.motivo})\n{e.TargetSite} {e.StackTrace}\n");
                sw.Close();
                return NotFound(e.motivo);
            }finally{
                sw.Close();
            }
        }
        [HttpPut("Put/Funcionario/{id}")]
        public IActionResult PutFuncionario(int id, [FromBody] Funcionario novoFuncionario){
            StreamWriter sw = new StreamWriter(@"Controllers\erros.txt", true);
            try{
                if(ModelState.IsValid){
                    using(var context = new SistemaNotaFiscal()){
                        var funcionario = context.Funcionarios.Find(id);
                        if(funcionario ==null) throw new ConcessionariaException("Não existe nenhum funcionario com esse id");
                        tratamento.FuncionarioTratamento(novoFuncionario);
                        context.Entry(funcionario).CurrentValues.SetValues(novoFuncionario);
                        context.SaveChanges();
                        return Ok();
                    }
                }else throw new ConcessionariaException("Erro ao criar objeto");
            }catch(ConcessionariaException e){
                sw.WriteLine($"Erro ao atualizar funcionario ({e.motivo})\n{e.TargetSite} {e.StackTrace}\n");
                sw.Close();
                return NotFound(e.motivo);
            }finally{
                sw.Close();
            }
        }

        [HttpPut("Put/Produto/{id}")]
        public IActionResult PutVeiculo(int id, [FromBody] Produto novoProduto){
            StreamWriter sw = new StreamWriter(@"Controllers\erros.txt", true);
            try{
                if(ModelState.IsValid){
                    using(var context = new SistemaNotaFiscal()){
                        var produto = context.Produtos.Find(id);
                        if(produto ==null) throw new ConcessionariaException("Não existe nenhum produto com esse id");
                        tratamento.ProdutoTratamento(novoProduto);
                        context.Entry(produto).CurrentValues.SetValues(novoProduto);
                        context.SaveChanges();
                        return Ok();
                    }
                }else throw new ConcessionariaException("Erro ao criar objeto");
            }catch(ConcessionariaException e){
                sw.WriteLine($"Erro ao atualizar funcionario ({e.motivo})\n{e.TargetSite} {e.StackTrace}\n");
                sw.Close();
                return NotFound(e.motivo);
            }finally{
                sw.Close();
            }
        }
        [HttpDelete("Delete/Cliente/{id}")]
        public IActionResult DeleteCliente(int id){
            StreamWriter sw = new StreamWriter(@"Controllers\erros.txt", true);
            try{
                if(ModelState.IsValid){
                    using(var context = new SistemaNotaFiscal()){
                        var cliente = context.Clientes.Find(id);
                        if(cliente == null) throw new ConcessionariaException("Não existe nenhum cliente com esse id");
                        var endereco = context.Enderecos.Find(cliente.IdEndereco);
                        
                        context.Enderecos.Remove(endereco);
                        context.Clientes.Remove(cliente);
                        context.SaveChanges();
                        return Ok();
                    }
                }else throw new ConcessionariaException("Erro ao criar objeto");
            }catch(ConcessionariaException e){
                sw.WriteLine($"Erro ao atualizar cliente ({e.motivo})\n{e.TargetSite} {e.StackTrace}\n");
                sw.Close();
                return NotFound(e.motivo);
            }finally{
                sw.Close();
            }
        }
    }
}