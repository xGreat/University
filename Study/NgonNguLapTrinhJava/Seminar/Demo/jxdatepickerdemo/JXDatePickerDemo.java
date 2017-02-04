/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package jxdatepickerdemo;

import java.awt.FlowLayout;
import java.awt.GridLayout;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.text.DateFormat;
import java.text.SimpleDateFormat;
import java.util.Date;
import javax.swing.JComboBox;
import javax.swing.JFrame;
import javax.swing.JLabel;
import javax.swing.JPanel;
import javax.swing.UIManager;
import org.jdesktop.swingx.JXDatePicker;

/**
 *
 * @author Bien
 */
public class JXDatePickerDemo extends javax.swing.JFrame {

    /**
     * Creates new form JXDatePickerDemo
     */
    public JXDatePickerDemo() {
        initComponents();
        setLookAndFeel ();
        
  Date date = new Date();
  jXDatePicker.setFormats("dd/MM/yyyy");
  jXDatePicker.setDate(date);

  // Create a frame window whose GUI presents the date picker and provides
  // a list of supported date formats.

        
  final JFrame frame = new JFrame ("Date Picker Month View Demo");
  frame.getContentPane ().setLayout (new GridLayout (2, 1));
  

  // Tell application to automatically exit when the user selects the Close
  // menu item from the frame window’s system menu.

  frame.setDefaultCloseOperation (JFrame.EXIT_ON_CLOSE);

  // Create GUI: date picker at the top and a combo box of date formats --
  // that the date picker supports for entering dates via the editor -- at
  // the bottom.

  JPanel panel = new JPanel ();
  panel.add (new JLabel ("Enter date"));
  final JXDatePicker datePicker;
  datePicker = new JXDatePicker ();
  ActionListener al = new ActionListener ()
       {
        public void actionPerformed (ActionEvent e)
        {
         System.out.println (datePicker.getDate ());
        }
       };
  datePicker.addActionListener (al);
  panel.add (datePicker);
  frame.getContentPane ().add (panel);

  panel = new JPanel ();
  panel.setLayout (new FlowLayout (FlowLayout.LEFT));
  panel.add (new JLabel ("Formats"));
  DateFormat [] dfs = datePicker.getFormats ();
  String [] fmts = new String [dfs.length];
  for (int i = 0; i < dfs.length; i++)
   fmts [i] = (dfs [i] instanceof SimpleDateFormat)
      ? ((SimpleDateFormat) dfs [i]).toPattern ()
      : dfs [i].toString ();
  panel.add (new JComboBox (fmts));
  frame.getContentPane ().add (panel);

  // Size frame window to fit the preferred size and layouts of its
  // components.

  frame.pack ();

  // Display GUI and start the AWT’s event-dispatching thread.

  frame.setVisible (true);
  
 }

 static void setLookAndFeel ()
 {
  try
  {
   // Return the name of the LookAndFeel class that implements the
   // native OS look and feel. If there is no such look and feel, return
   // the name of the default cross platform LookAndFeel class.

   String slafcn = UIManager.getSystemLookAndFeelClassName ();

   // Set the current look and feel to the look and feel identified by
   // the LookAndFeel class name.

   UIManager.setLookAndFeel (slafcn);
  }
  catch(Exception e)
  {
  }
    }

    /**
     * This method is called from within the constructor to initialize the form.
     * WARNING: Do NOT modify this code. The content of this method is always
     * regenerated by the Form Editor.
     */
    @SuppressWarnings("unchecked")
    // <editor-fold defaultstate="collapsed" desc="Generated Code">//GEN-BEGIN:initComponents
    private void initComponents() {

        jXDatePicker = new org.jdesktop.swingx.JXDatePicker();

        setDefaultCloseOperation(javax.swing.WindowConstants.EXIT_ON_CLOSE);

        jXDatePicker.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                jXDatePickerActionPerformed(evt);
            }
        });

        javax.swing.GroupLayout layout = new javax.swing.GroupLayout(getContentPane());
        getContentPane().setLayout(layout);
        layout.setHorizontalGroup(
            layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(layout.createSequentialGroup()
                .addGap(136, 136, 136)
                .addComponent(jXDatePicker, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
                .addContainerGap(152, Short.MAX_VALUE))
        );
        layout.setVerticalGroup(
            layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(layout.createSequentialGroup()
                .addGap(130, 130, 130)
                .addComponent(jXDatePicker, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
                .addContainerGap(148, Short.MAX_VALUE))
        );

        pack();
    }// </editor-fold>//GEN-END:initComponents

    private void jXDatePickerActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_jXDatePickerActionPerformed
        // TODO add your handling code here:
    }//GEN-LAST:event_jXDatePickerActionPerformed

    /**
     * @param args the command line arguments
     */
    public static void main(String args[]) {
        /* Set the Nimbus look and feel */
        //<editor-fold defaultstate="collapsed" desc=" Look and feel setting code (optional) ">
        /* If Nimbus (introduced in Java SE 6) is not available, stay with the default look and feel.
         * For details see http://download.oracle.com/javase/tutorial/uiswing/lookandfeel/plaf.html 
         */
        try {
            for (javax.swing.UIManager.LookAndFeelInfo info : javax.swing.UIManager.getInstalledLookAndFeels()) {
                if ("Nimbus".equals(info.getName())) {
                    javax.swing.UIManager.setLookAndFeel(info.getClassName());
                    break;
                }
            }
        } catch (ClassNotFoundException ex) {
            java.util.logging.Logger.getLogger(JXDatePickerDemo.class.getName()).log(java.util.logging.Level.SEVERE, null, ex);
        } catch (InstantiationException ex) {
            java.util.logging.Logger.getLogger(JXDatePickerDemo.class.getName()).log(java.util.logging.Level.SEVERE, null, ex);
        } catch (IllegalAccessException ex) {
            java.util.logging.Logger.getLogger(JXDatePickerDemo.class.getName()).log(java.util.logging.Level.SEVERE, null, ex);
        } catch (javax.swing.UnsupportedLookAndFeelException ex) {
            java.util.logging.Logger.getLogger(JXDatePickerDemo.class.getName()).log(java.util.logging.Level.SEVERE, null, ex);
        }
        //</editor-fold>

        /* Create and display the form */
        java.awt.EventQueue.invokeLater(new Runnable() {
            public void run() {
                new JXDatePickerDemo().setVisible(true);
            }
        });
    }

    // Variables declaration - do not modify//GEN-BEGIN:variables
    private org.jdesktop.swingx.JXDatePicker jXDatePicker;
    // End of variables declaration//GEN-END:variables
}