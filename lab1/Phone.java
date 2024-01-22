import java.io.*; 
import java.util.*; 

public class Phone {
	public String phoneName(String phone) {
 
	String name= new String(); 
	String line;
	
	try { 
			FileReader fr = new 
			FileReader("C:/development/labs/lab1/phones.txt"); 
			BufferedReader in = new BufferedReader(fr);
			
			while ((line=in.readLine())!=null){ 
				String[] str = line.split(" "); 
		
				if (str[0].equals(phone)){
					name=str[1];
					
					break;
				} else {
				name="NO SUCH TELEPHONE"; 
				}
			}
		} catch(IOException e){ 
			e.printStackTrace(); 
		} 
 
		return name; 
	} 
	
	public String namePhone(String name){ 
 
		String phone= new String();
		String line; 
		
		try { 
			FileReader fr = new 
			FileReader("C:/development/labs/lab1/phones.txt");
			BufferedReader in = new BufferedReader(fr); 
 
			while ((line=in.readLine())!=null){ 
				String[] str = line.split(" "); 
				if (str[1].equals(name)){
					phone=str[0]; 
					
					break;
				} else { 
					phone="NO SUCH NAME"; 
				}
			}
		} catch(IOException e){ 
			e.printStackTrace(); 
		} 
		
		return phone; 
	}  
}